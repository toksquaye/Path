# Path
provides change directory (cd) function for an abstract file system.

Developer : Tokunbo Quaye
Date : January 2015

Code written in C# using VS 2013 .NET Framework 4.5

Description : A function that provides change directory (cd) function for an abstract file system.

Notes:
- Root path is '/'.
- Path separator is '/'.
- Parent directory is addressable as "..".
- Directory names consist only of English alphabet letters (A-Z and a-z).

For example, new Path("/a/b/c/d").Cd("../x").CurrentPath should return "/a/b/c/x". 

