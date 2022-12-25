//       ========================
//       Lilikoi.Core::InjectionGenerator.cs
//       Distributed under the MIT License.
//
// ->    Created: 22.12.2022
// ->    Bumped: 22.12.2022
//
// ->    Purpose:
//
//
//       ========================
#region

using System;
using System.Linq.Expressions;
using System.Reflection;

using Lilikoi.Core.Attributes;
using Lilikoi.Core.Attributes.Builders;
using Lilikoi.Core.Context;

#endregion

namespace Lilikoi.Core.Builder.Mahogany.Generator;

internal static class InjectionGenerator
{
	internal static MethodInfo LkInjectionBuilderAttribute_Build = typeof(LkInjectionBuilderAttribute).GetMethod("Build");

	internal static MethodInfo LkInjectionAttribute_Inject = typeof(LkInjectionAttribute).GetMethod("Inject");
	internal static MethodInfo LkInjectionAttribute_Deject = typeof(LkInjectionAttribute).GetMethod("Deject");
	internal static MethodInfo LkInjectionAttribute_IsInjectable = typeof(LkInjectionAttribute).GetMethod("IsInjectable");

	#region Build

	internal static Expression Builder(LkInjectionBuilderAttribute builderAttribute, Mount mount)
	{
		return
			Expression.Call(
				Expression.Constant(builderAttribute, typeof(LkInjectionBuilderAttribute)),
				LkInjectionBuilderAttribute_Build, Expression.Constant(mount));
	}

	#endregion

	#region Inject

	/// <summary>
	///     Returns an expression value which creates an injectable value.
	/// </summary>
	/// <param name="result"></param>
	/// <returns></returns>
	public static Expression InjectValue(MahoganyMethod method, Expression attribute, Type result)
	{
		//	var0 = injectionAttribute.Inject();	//	EXCEPTION POSSIBLE HERE
		//	return var0 as result //  NULL REFERENCE POSSIBLE HERE

		var invoke = LkInjectionAttribute_Inject.MakeGenericMethod(result);


		return //Expression.TypeAs(
			Expression.Call(attribute, invoke, method.Named(MahoganyConstants.MOUNT_VAR)); //, result);
	}

	/// <summary>
	///     Returns a binary expression that sets the property on the source to the injected value
	/// </summary>
	/// <param name="source">An expression that resolves to the type being injected.</param>
	/// <param name="property"></param>
	/// <returns></returns>
	public static Expression InjectValueAsProperty(MahoganyMethod method, Expression attribute, ParameterExpression source, PropertyInfo property)
	{
		//	if (InjectValue == null)
		//		throw ArgumentNullException("builder")
		//	object[property] = InjectValue<property>()	//	EXCEPTION POSSIBLE HERE
		//	if (object[property] == null)
		//		throw ArgumentNullException(...)	//	duh...

		var setter = method.AsVariable(InjectValue(method, attribute, property.PropertyType), out var value);

		return Expression.Block(
			CommonGenerator.GuardAgainstNull(attribute, new ArgumentNullException($"__builder {attribute.Type.Name}")),
			setter,
			CommonGenerator.GuardAgainstNull(value, new ArgumentNullException(property.Name, $"Injectable {attribute.Type.Name} returned null value.")),
			Expression.Assign(
				Expression.Property(source, property.SetMethod),
				value
				)
			);
	}

	public static Expression InjectValueAsField(MahoganyMethod method, Expression attribute, ParameterExpression source, FieldInfo field)
	{
		//	if (InjectValue == null)
		//		throw ArgumentNullException("builder")
		//	object[property] = InjectValue<property>()	//	EXCEPTION POSSIBLE HERE
		//	if (object[property] == null)
		//		throw ArgumentNullException(...)	//	duh...

		var setter = method.AsVariable(InjectValue(method, attribute, field.FieldType), out var value);

		return Expression.Block(
			CommonGenerator.GuardAgainstNull(attribute, new ArgumentNullException($"__builder {attribute.Type.Name}")),
			setter,
			CommonGenerator.GuardAgainstNull(value, new ArgumentNullException(field.Name, $"Injectable {attribute.Type.Name} returned null value.")),
			Expression.Assign(
				Expression.Field(source, field),
				value
				)
			);
	}

	#region Headless

	public static Expression InjectValueHeadless(Expression attribute, Type result, ParameterExpression mount)
	{
		//	var0 = injectionAttribute.Inject();	//	EXCEPTION POSSIBLE HERE
		//	return var0 as result //  NULL REFERENCE POSSIBLE HERE

		var invoke = LkInjectionAttribute_Inject.MakeGenericMethod(result);


		return //Expression.TypeAs(
			Expression.Call(attribute, invoke, mount); //, result);
	}

	public static Expression InjectValueAsPropertyHeadless(Expression attribute, ParameterExpression host, ParameterExpression mount, PropertyInfo property, out ParameterExpression value)
	{
		var setter = CommonGenerator.ToVariable(InjectValueHeadless(attribute, property.PropertyType, mount), out value);

		return Expression.Block(
			CommonGenerator.GuardAgainstNull(attribute, new ArgumentNullException($"__builder {attribute.Type.Name}")),
			setter,
			CommonGenerator.GuardAgainstNull(value, new ArgumentNullException(property.Name, $"Injectable {attribute.Type.Name} returned null value.")),
			Expression.Assign(
				Expression.Property(host, property),
				value
				)
			);
	}

	public static Expression InjectValueAsFieldHeadless(Expression attribute, ParameterExpression host, ParameterExpression mount, FieldInfo field, out ParameterExpression value)
	{
		var setter = CommonGenerator.ToVariable(InjectValueHeadless(attribute, field.FieldType, mount), out value);

		return Expression.Block(
			CommonGenerator.GuardAgainstNull(attribute, new ArgumentNullException($"__builder {attribute.Type.Name}")),
			setter,
			CommonGenerator.GuardAgainstNull(value, new ArgumentNullException(field.Name, $"Injectable {attribute.Type.Name} returned null value.")),
			Expression.Assign(
				Expression.Field(host, field),
				value
				)
			);
	}

	#endregion

	#endregion


	#region Deject

	/// <summary>
	///     Create a function call that dejects a value
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	public static Expression DejectValue(MahoganyMethod method, Expression attribute, Expression value)
	{
		//	injectionAttribute.Deject(var0)	//	EXCEPTION POSSIBLE HERE

		var invoke = LkInjectionAttribute_Deject.MakeGenericMethod(value.Type);

		return Expression.Call(attribute, invoke, method.Named(MahoganyConstants.MOUNT_VAR), value);
	}

	/// <summary>
	///     Create a block that reads a property and dejects it's value
	/// </summary>
	/// <param name="source"></param>
	/// <param name="property"></param>
	/// <returns></returns>
	public static Expression DejectValueAsProperty(MahoganyMethod method, Expression attribute, ParameterExpression source, PropertyInfo property)
	{
		//	DejectValue<property>(object[parameter])	//	EXCEPTION POSSIBLE HERE
		return DejectValue(
			method,
			attribute,
			Expression.Property(source, property)
			);
	}

	public static Expression DejectValueAsField(MahoganyMethod method, Expression attribute, ParameterExpression source, FieldInfo field)
	{
		//	DejectValue<property>(object[parameter])	//	EXCEPTION POSSIBLE HERE
		return DejectValue(
			method,
			attribute,
			Expression.Field(source, field)
			);
	}

	#region Headless

	public static Expression DejectValueHeadless(ParameterExpression mountVar, Expression attribute, Expression value)
	{
		//	injectionAttribute.Deject(var0)	//	EXCEPTION POSSIBLE HERE

		var invoke = LkInjectionAttribute_Deject.MakeGenericMethod(value.Type);

		return Expression.Call(attribute, invoke, mountVar, value);
	}

	public static Expression DejectValueAsFieldHeadless(ParameterExpression mountVar, Expression attribute, ParameterExpression source, FieldInfo field)
	{
		//	DejectValue<property>(object[parameter])	//	EXCEPTION POSSIBLE HERE
		return DejectValueHeadless(
			mountVar,
			attribute,
			Expression.Field(source, field)
			);
	}

	#endregion

	#endregion
}
