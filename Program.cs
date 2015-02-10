/* Tokunbo Quaye
 * January 2015
 * 
 * Write a function that provides change directory (cd) function for an abstract file system.

Notes:
- Root path is '/'.
- Path separator is '/'.
- Parent directory is addressable as "..".
- Directory names consist only of English alphabet letters (A-Z and a-z).

For example, new Path("/a/b/c/d").Cd("../x").CurrentPath should return "/a/b/c/x". 
* */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace Path
{
    public class Path
    {
        public string CurrentPath { get; private set; }
        private const string PathSeparator = "/";
        
        //current path directories in a list structure. this eases manipulation of directories
        private List<string> dirPath = new List<string>();
        
        internal enum PathType
        {
            NewPath, //constructor path
            CDPath  //change directory path
        }

        public Path(string path)
        {
            ValidatePath(path);
                        
        }

        

        /* confirm that path is valid. default to root if invalid. place path in array*/
        private void ValidatePath(string path)
        {
            if (Validate(path, PathType.NewPath))
            {
                this.CurrentPath = path;
                //create path list only if its not the root
                if (path.Length > 1)
                {
                    string[] tempSplit = Regex.Split(path, PathSeparator).Skip(1).ToArray();
                    CreatePathList(tempSplit);
                }
            }
            else
            {
                Console.WriteLine("Invalid file path : Default to root");
                this.CurrentPath = "/";
            }
        }

        /*confirm the given path format is valid. */
        private bool Validate(string path, PathType pathtype)
        {
            switch (pathtype)
            {
                case PathType.NewPath:
                    return Regex.IsMatch(path, @"^/[A-Za-z]*(/[A-Za-z]+)*$");

                case PathType.CDPath:
                    return Regex.IsMatch(path, @"^(..)|^[A-Za-z](/[A-Za-z])*$");

                default:
                    return false;
            }
        }
        /*create a list of the directories in CurrentPath*/
        private void CreatePathList(string[] pathSplit)
        {
            foreach (string p in pathSplit)
            {
                dirPath.Add(p);
            }
        }

        /* change directory */
        public Path Cd(string newPath)
        {
          
            //if cd command is invalid, notify user and exit.
            if (!Validate(newPath,PathType.CDPath))
            {
                Console.WriteLine("Invalid CD command. Directory Unchanged");
                return new Path(this.CurrentPath);
            }

            //split newPath into an array of directory names
            var tempSplit = Regex.Split(newPath, PathSeparator);

            //change current directory based on specified input from newPath
            foreach (var dir in tempSplit)
            {
                switch (dir)
                {
                    case "..": if (dirPath.Count > 0) //do this if current dir is not root
                        {
                            dirPath.RemoveAt(dirPath.Count - 1);
                        }
                        else
                        { 
                            Console.WriteLine("Unable to change directory as specified");
                            return new Path(this.CurrentPath);
                        } 
                        break;
                    default: dirPath.Add(dir); break;
                }
            }

            //reconstruct directory string
            var newDirPath = new StringBuilder();
            foreach(var dir in dirPath)
            {
                newDirPath.Append(PathSeparator);
                newDirPath.Append(dir);
            }
            return new Path(newDirPath.ToString());

        }
            

        public static void Main(string[] args)
        {
            Path path = new Path("/ac/d");
            Console.WriteLine(path.Cd("c/x").CurrentPath);
            Console.WriteLine(String.Empty);

            Path path2 = new Path("/");
            Console.WriteLine(path2.Cd("a/b/c/d/e/..").CurrentPath);
            Console.WriteLine(String.Empty);

            Path path3 = new Path("ab/d");
            Console.WriteLine(path3.Cd("a/b/c/d/e/..").CurrentPath);
            Console.WriteLine(String.Empty);

            Path path4 = new Path("/doc/for/you");
            Console.WriteLine(path4.Cd("..").CurrentPath);
            Console.WriteLine(path4.Cd("me/and/you").CurrentPath);
            Console.ReadLine();

        }
    }
}
