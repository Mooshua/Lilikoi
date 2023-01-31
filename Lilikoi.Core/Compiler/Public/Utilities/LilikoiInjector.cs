//       ========================
//       Lilikoi.Core::LilikoiInjector.cs
//       Distributed under the MIT License.
//
// ->    Created: 23.12.2022
// ->    Bumped: 23.12.2022
//
// ->    Purpose:
//
//
//       ========================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using Lilikoi.Core.Attributes.Builders;
using Lilikoi.Core.Compiler.Mahogany;
using Lilikoi.Core.Compiler.Mahogany.Generator;
using Lilikoi.Core.Context;

namespace Lilikoi.Core.Compiler.Public.Utilities;

public static class LilikoiInjector
{
	internal static void InjectsForClass(List<Expression> injects, List<Expression> dejects, List<ParameterExpression> variables, ParameterExpression host, ParameterExpression mount, Mount mountVal, Type type)
	{
		foreach (FieldInfo fieldInfo in type.GetFields())
		foreach (LkInjectionBuilderAttribute attribute in
		         fieldInfo.GetCustomAttributes()
			         .OfType<LkInjectionBuilderAttribute>())
		{

			MahoganyValidator.ValidateInjection(attribute, fieldInfo, mountVal);

			var setter = CommonGenerator.ToVariable(
				InjectionGenerator.Builder(attribute, mountVal),
				out var instance
				);

			injects.Add(setter);
			injects.Add(InjectionGenerator.InjectValueAsFieldHeadless(instance, host, mount, fieldInfo, out var temp0));

			dejects.Add(setter);
			dejects.Add(InjectionGenerator.DejectValueAsFieldHeadless(mount, instance, host, fieldInfo));


			variables.Add(instance);
			variables.Add(temp0);
		}
	}

	internal static Injector<THost> InjectorForClass<THost>( Mount mount )
	{
		var host = typeof(THost);
		List<Expression> body = new List<Expression>();
		List<Expression> dejectBody = new List<Expression>();

		List<ParameterExpression> variables = new List<ParameterExpression>();
		ParameterExpression hostVar = Expression.Parameter(host, "__host");
		ParameterExpression mountVar = Expression.Parameter(typeof(Mount), "__mount");

		body.Add(Expression.Assign(mountVar, Expression.Constant(mount)));

		InjectsForClass(body, dejectBody, variables, hostVar, mountVar, mount, host);

		var injectsBody = Expression.Block(variables, body);
		var allBody = Expression.Block(new[] { mountVar }, injectsBody);

		var lambda = Expression.Lambda<Injector<THost>>(allBody, $"LkInject:'{host.FullName}'", new[] { hostVar });

#if !DEBUG
		return lambda.Compile(false);
#endif

#if DEBUG
		return lambda.Compile(true);
#endif
	}

	internal static Dejector<THost> DejectorForClass<THost>(Mount mount)
	{
		var host = typeof(THost);
		List<Expression> injectBody = new List<Expression>();
		List<Expression> body = new List<Expression>();

		List<ParameterExpression> variables = new List<ParameterExpression>();
		ParameterExpression hostVar = Expression.Parameter(host, "__host");
		ParameterExpression mountVar = Expression.Parameter(typeof(Mount), "__mount");

		body.Add(Expression.Assign(mountVar, Expression.Constant(mount)));

		InjectsForClass(injectBody, body, variables, hostVar, mountVar, mount, host);

		var injectsBody = Expression.Block(variables, body);
		var allBody = Expression.Block(new[] { mountVar }, injectsBody);

		var lambda = Expression.Lambda<Dejector<THost>>(allBody, $"LkInject:'{host.FullName}'", new[] { hostVar });

#if !DEBUG
		return lambda.Compile(false);
#endif

#if DEBUG
		return lambda.Compile(true);
#endif
	}

	public delegate void Injector<T>(T obj);

	public delegate void Dejector<T>(T obj);


	public static Injector<ToInject> CreateInjector<ToInject>(Mount mount)
		where ToInject: class
	{
		return InjectorForClass<ToInject>(mount);
	}

	public static Injector<ToInject> CreateInjector<ToInject>()
		where ToInject: class
	{
		return CreateInjector<ToInject>(new Mount());
	}

	public static Dejector<ToInject> CreateDejector<ToInject>(Mount mount)
		where ToInject: class
	{
		return DejectorForClass<ToInject>(mount);
	}

	public static Dejector<ToInject> CreateDejector<ToInject>()
		where ToInject: class
	{
		return DejectorForClass<ToInject>(new Mount());
	}

	public static void Inject<T>(Mount cache, T self)
		where T : class
	{
		if (!cache.Has<Injector<T>>())
			cache.Store(CreateInjector<T>());

		cache.Get<Injector<T>>()!(self);
	}

	public static void Deject<T>(Mount cache, T self)
		where T : class
	{
		if (!cache.Has<Dejector<T>>())
			cache.Store(CreateDejector<T>());

		cache.Get<Dejector<T>>()!(self);
	}
}
