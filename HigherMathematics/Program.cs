using BenchmarkDotNet.Running;
using Mathematics;

namespace HigherMathematics
{
    public static class Program
    {
        public static void Main(string[] args)
        {

            //BenchmarkRunner.Run<BenchmarkMathematics>();
             var matrix = new Matrix(
                 new double[,] { {1, -2, 0, 1, 2 }, 
                                 {2, -2, 3, -1, 1 },
                                 {3, 1, -3, 1, 2 },
                                 {4, 2, -3, -4, -5 },
                                 {5, 4, -1, 0, 0 } });
             var answer = new Matrix(new double[,]
             {
                 {2 },
                 {3 },
                 {-2},
                 {-1},
                 {1}
             });
             var matrix1 = new Matrix(
                 new double[,] { { 1, 2, -1 }, 
                                 { 2, 5, -6 },
                                 { 3, 8, -10} });
             var answer1 = new Matrix(new double[,]
             {
                 { 3 },
                 { 1 },
                 { 1 },
             });
             var matrix2 = new Matrix(
                 new double[,] { {1, 2, -4}, 
                                 {2, 1, -5},
                                 {1, -1, -1} });
             var answer2 = new Matrix(new double[,]
             {
                 { 1 },
                 {-1 },
                 {-2 },
             });
            
             var matrix3 = new Matrix(
                 new double[,] { { 1, 2, 3, 4, 5 }, 
                                 { 2, 3, 4, 5, 6 } });
             var answer3 = new Matrix(new double[,]
             {
                 {1},
                 {2},
             });
            
        }
    }
}