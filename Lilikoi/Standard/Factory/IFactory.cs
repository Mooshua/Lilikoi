//       ========================
//       Lilikoi::IFactory.cs
//
// ->    Created: 01.02.2023
// ->    Bumped: 01.02.2023
//
// ->    Purpose:
//
//
//       ========================
namespace Lilikoi.Standard;

public interface IFactory<TProduct>
{
	public TProduct Create();
}
