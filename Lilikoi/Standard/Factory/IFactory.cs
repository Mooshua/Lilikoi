//       ========================
//       Lilikoi::IFactory.cs
//       (c) 2023. Distributed under the MIT License
//
// ->    Created: 01.02.2023
// ->    Bumped: 06.02.2023
//       ========================
namespace Lilikoi.Standard.Factory;

public interface IFactory<TProduct>
{
	public TProduct Create();
}
