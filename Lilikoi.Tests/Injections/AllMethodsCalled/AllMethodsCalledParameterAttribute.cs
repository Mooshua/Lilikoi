//       ========================
//       Lilikoi.Tests::AllMethodsCalledParameterAttribute.cs
//       (c) 2023. Distributed under the MIT License
// 
// ->    Created: 22.12.2022
// ->    Bumped: 06.02.2023
//       ========================
#region

using Lilikoi.Attributes.Typed;
using Lilikoi.Context;

#endregion

namespace Lilikoi.Tests.Injections.AllMethodsCalled;

public class AllMethodsCalledParameterAttribute : LkTypedParameterAttribute<object, AllMethodsCalledTest.AllMethodsCalledCounter>
{
	public override object Inject(Mount context, AllMethodsCalledTest.AllMethodsCalledCounter input)
	{
		input.ParameterCalled = true;
		return new object();
	}
}