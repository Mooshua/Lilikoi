//       ========================
//       Lilikoi.Core::DebugGenerator.cs
//       Distributed under the MIT License.
//
// ->    Created: 24.12.2022
// ->    Bumped: 24.12.2022
//
// ->    Purpose:
//
//
//       ========================
using System.Linq.Expressions;

namespace Lilikoi.Compiler.Mahogany.Generator;

public static class DebugGenerator
{
	public static Expression Quick(int startline, int endline, string file = "LilikoiMahogany.cs")
	{
		return Expression.DebugInfo(Expression.SymbolDocument(file), startline, 0, endline, 1);
	}
	public static Expression Quick(int line, string file = "LilikoiMahogany.cs")
	{
		return Expression.DebugInfo(Expression.SymbolDocument(file), line, 0, line, 1);
	}
}
