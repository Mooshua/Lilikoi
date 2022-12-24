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

using AgileObjects.ReadableExpressions;

using Lilikoi.Core.Attributes;
using Lilikoi.Core.Attributes.Builders;
using Lilikoi.Core.Builder.Mahogany;
using Lilikoi.Core.Builder.Mahogany.Generator;
using Lilikoi.Core.Context;

namespace Lilikoi.Core.Builder.Public.Utilities;

public static class LilikoiInjector
{
	internal static void InjectsForClass(List<Expression> injects, List<ParameterExpression> variables, ParameterExpression host, ParameterExpression mount, Type type)
	{
		foreach (PropertyInfo propertyInfo in type.GetProperties())
		foreach (LkInjectionBuilderAttribute attribute in
		         propertyInfo.GetCustomAttributes()
			         .OfType<LkInjectionBuilderAttribute>())
		{

			MahoganyValidator.ValidateInjection(attribute, propertyInfo);

			var setter = CommonGenerator.ToVariable(
				InjectionGenerator.Builder(attribute),
				out var instance
				);

			injects.Add(setter);
			injects.Add(InjectionGenerator.InjectValueAsPropertyHeadless(instance, host, mount, propertyInfo, out var temp));

			variables.Add(instance);
			variables.Add(temp);
		}
	}

	internal static Action<THost> InjectorForClass<THost>( Mount mount )
	{
		var host = typeof(THost);
		List<Expression> body = new List<Expression>();
		List<ParameterExpression> variables = new List<ParameterExpression>();
		ParameterExpression hostVar = Expression.Parameter(host, "__host");
		ParameterExpression mountVar = Expression.Parameter(typeof(Mount), "__mount");

		body.Add(Expression.Assign(mountVar, Expression.Constant(mount)));

		InjectsForClass(body, variables, hostVar, mountVar, host);

		var injectsBody = Expression.Block(variables, body);
		var allBody = Expression.Block(new[] { mountVar }, injectsBody);

		var lambda = Expression.Lambda<Action<THost>>(allBody, $"LkInject:'{host.FullName}'", new[] { hostVar });

		return lambda.Compile(false);
	}

	public static Action<ToInject> CreateInjector<ToInject>(Mount mount)
		where ToInject: class
	{
		return InjectorForClass<ToInject>(mount);
	}

	public static Action<ToInject> CreateInjector<ToInject>()
		where ToInject: class
	{
		return CreateInjector<ToInject>(new Mount());
	}
}
