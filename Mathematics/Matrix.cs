namespace Mathematics;
public sealed class Matrix
{
    public Matrix(double[,] matrix)
    {
        Body = matrix;
        CountOfRow = matrix.GetUpperBound(0) + 1;
        CountOfColumn = matrix.Length / (CountOfRow);
    }
    
    private Matrix(int countOfRow, int countOfColumn)
    {
        Body = new double[countOfRow,countOfColumn];
        CountOfRow = countOfRow;
        CountOfColumn = countOfColumn;
    }
    
    public static Matrix GenerateZeroMatrix(int countOfRows, int countOfColumns) =>
        new Matrix(countOfRows,countOfColumns);

    public static Matrix GenerateUnitMatrix(int countOfRows, int countOfColumns)
    {
        var resultMatrix = GenerateZeroMatrix(countOfRows, countOfColumns);
        for (var i = 0; i < countOfRows; i++)
        {
            for (var j = 0; j < countOfRows; j++)
            {
                resultMatrix.Body[i, j] = i == j ? 1 : 0;
            }
        }
        return resultMatrix;
    }
    
    public static Matrix GenerateIntRandomMatrix(int countOfRows, int countOfColumns)
    {
        var resultMatrix = GenerateZeroMatrix(countOfRows, countOfColumns);
        var random = new Random();
        for (var i = 0; i < countOfRows; i++)
        {
            for (var j = 0; j < countOfRows; j++)
            {
                resultMatrix.Body[i, j] = random.Next(-10000, 10000);
            }
        }
        return resultMatrix;
    }
    
    public static void Print(Matrix matrix)
    {
        for (var i = 0; i < matrix.CountOfRow; i++)
        {
            for (var j = 0; j < matrix.CountOfColumn; j++)
            {
                Console.Write($"{matrix.Body[i, j]} ");
            }
            Console.WriteLine();
        }
    }
    
    public static Matrix operator +(Matrix firstMatrix, Matrix secondMatrix)
    {
        var resultMatrix = GenerateZeroMatrix(firstMatrix.CountOfRow, firstMatrix.CountOfColumn);
        if (firstMatrix.CountOfRow != secondMatrix.CountOfRow ||
            firstMatrix.CountOfColumn != secondMatrix.CountOfColumn) return resultMatrix;
        for (var i = 0; i < firstMatrix.CountOfRow; i++)
        {
            for (var j = 0; j < firstMatrix.CountOfColumn; j++)
            {
                resultMatrix.Body[i, j] = firstMatrix.Body[i, j] + secondMatrix.Body[i, j];
            }
        }
        return resultMatrix;
    }

    public static Matrix operator -(Matrix firstMatrix, Matrix secondMatrix)
    {
        var resultMatrix = GenerateZeroMatrix(firstMatrix.CountOfRow, firstMatrix.CountOfColumn);
        if (firstMatrix.CountOfRow != secondMatrix.CountOfRow ||
            firstMatrix.CountOfColumn != secondMatrix.CountOfColumn) return resultMatrix;
        for (var i = 0; i < firstMatrix.CountOfRow; i++)
        {
            for (var j = 0; j < firstMatrix.CountOfColumn; j++)
            {
                resultMatrix.Body[i, j] = firstMatrix.Body[i, j] - secondMatrix.Body[i, j];
            }
        }
        return resultMatrix;
    }
    
    public static Matrix operator *(double number, Matrix matrix) => Multiply(matrix, number);
        
    public static Matrix operator *(Matrix matrix, double number) => Multiply(matrix, number);

    private static Matrix Multiply(Matrix matrix, double number)
    {
        var resultMatrix = GenerateZeroMatrix(matrix.CountOfRow, matrix.CountOfColumn);
        for (var i = 0; i < matrix.CountOfRow; i++)
        {
            for (var j = 0; j < matrix.CountOfColumn; j++)
            {
                resultMatrix.Body[i, j] = number * matrix.Body[i, j];
            }
        }
        return resultMatrix;
    }
        
    
    public static Matrix operator *(Matrix firstMatrix, Matrix secondMatrix)
    {
        var resultMatrix = GenerateZeroMatrix(firstMatrix.CountOfRow, firstMatrix.CountOfColumn);
        for (var i = 0; i < firstMatrix.CountOfRow; i++)
        {
            for (var j = 0; j < secondMatrix.CountOfColumn; j++)
            {
                for (int z = 0, d = 0; z < secondMatrix.CountOfRow && d < firstMatrix.CountOfColumn; z++, d++)
                {
                    resultMatrix.Body[i, j] += secondMatrix.Body[z, j] * firstMatrix.Body[i, d];
                }
            }
        }
        return resultMatrix;
    }

    public static Matrix Transpose(Matrix matrix)
    {
        var resultMatrix = GenerateZeroMatrix(matrix.CountOfRow, matrix.CountOfColumn);
        for (var i = 0; i < matrix.CountOfRow; i++)
        {
            for (var j = 0; j < matrix.CountOfColumn; j++)
            {
                resultMatrix.Body[j, i] = matrix.Body[i, j];
            }
        }
        return resultMatrix;
    }

    public double СalculateMinorByCoordinates(int horizontalCoordinate, int verticalCoordinate)
    {
        int x = 0, y = 0;
        var resultMatrix = GenerateZeroMatrix(CountOfRow - 1, CountOfRow - 1);
        for (var i = 0; i < CountOfRow; i++)
        {
            for (var j = 0; j < CountOfColumn; j++)
            {
                if (j == verticalCoordinate || i == horizontalCoordinate) continue;
                resultMatrix.Body[x, y] = Body[i, j];
                if (++y <= resultMatrix.CountOfRow - 1) continue;
                y = 0;
                x++;
            }
        }
        return resultMatrix.Determinant;
    }

    public double CalculateTheAlgebraicComplement(int horizontalCoordinate, int verticalCoordinate) =>
        Math.Pow(-1, horizontalCoordinate + verticalCoordinate) * СalculateMinorByCoordinates(horizontalCoordinate, verticalCoordinate);

    public static Matrix GetTheMatrixOfAlgebraicComplement(Matrix matrix)
    {
        var resultMatrix = GenerateZeroMatrix(matrix.CountOfRow, matrix.CountOfColumn);
        for (var i = 0; i < matrix.CountOfRow; i++)
        {
            for (var j = 0; j < matrix.CountOfColumn; j++)
            {
                resultMatrix.Body[i, j] = matrix.CalculateTheAlgebraicComplement(i, j);
            }
        }
        return resultMatrix;
    }

    public static Matrix СalculateByKramerMethod(Matrix matrixOfCoefficients, Matrix responseMatrix)
    {
        if (matrixOfCoefficients.Determinant == 0) 
            throw new Exception("Determinant is 0"); 
        var resultMatrix = GenerateZeroMatrix(matrixOfCoefficients.CountOfRow, 1);
        var determinant = matrixOfCoefficients.Determinant;
        for (var j = 0; j < matrixOfCoefficients.CountOfColumn; j++)
        {
            SwitchPlaces(matrixOfCoefficients.Body, responseMatrix.Body, j);
            resultMatrix.Body[j,0] = matrixOfCoefficients.Determinant / determinant;
            SwitchPlaces(matrixOfCoefficients.Body, responseMatrix.Body, j);
        }
        return resultMatrix;
    }
    private static void SwitchPlaces(double[,] a, double[,] b, int coordinate)
    {
        for (var i = 0; i < a.GetUpperBound(0) + 1; i++)
        {
            (a[i, coordinate], b[i, 0]) = (b[i, 0], a[i, coordinate]);
        }
    }
    public static Matrix LeadToTrapezoidalAppearance(Matrix matrix)
    {
        var resultMatrix = GenerateZeroMatrix(matrix.CountOfRow, matrix.CountOfColumn);

        for (var i = 0; i < matrix.CountOfRow; i++)
        {
            for (var j = 0; j < matrix.CountOfColumn; j++)
            {
                resultMatrix.Body[i, j] = matrix.Body[i, j];
            }  
        }    
            
        for (var z = 0; z < matrix.CountOfRow - 1; z++)
        {
            for (var i = z + 1; i < matrix.CountOfRow; i++)
            {
                var multiplicationFactor = resultMatrix.Body[i, z] / resultMatrix.Body[z, z];
                for (var j = z; j < matrix.CountOfColumn; j++)
                {
                    resultMatrix.Body[i, j] -= multiplicationFactor * resultMatrix.Body[z, j];
                }
            }
        }
        return resultMatrix;
    }

    public static Matrix CreateAnExtendedMatrix(Matrix matrixOfCoefficients, Matrix answer)
    {
        var extendedMatrix = GenerateZeroMatrix(matrixOfCoefficients.CountOfRow, matrixOfCoefficients.CountOfColumn + 1);
        for (var i = 0; i < matrixOfCoefficients.CountOfRow; i++)
        {
            for (var j = 0; j < matrixOfCoefficients.CountOfColumn; j++)
            {
                extendedMatrix.Body[i, j] = matrixOfCoefficients.Body[i, j];
            }
            extendedMatrix.Body[i, extendedMatrix.CountOfColumn - 1] = answer.Body[i, 0];
        }
        return extendedMatrix;
    }

    public double Determinant
    {
        get
        {
            if (CountOfRow == 2)
                return (Body[0, 0] * Body[1, 1] -
                        Body[0, 1] * Body[1, 0]);
            var determinant = 0.0;
            for (var z = 0; z < CountOfRow; z++)
            {
                determinant += Body[0, z] * CalculateTheAlgebraicComplement(0, z);
            }
            return determinant;
        }
    }

    public int Rank
    {
        get
        {
            var rank = Math.Min(LeadToTrapezoidalAppearance(this).CountOfRow, LeadToTrapezoidalAppearance(this).CountOfColumn);
            for (var i = 0; i < CountOfRow; i++)
            {
                for (var j = 0; j < CountOfColumn; j++)
                {
                    if (Body[i, j] != 0) break;
                    if (j == LeadToTrapezoidalAppearance(this).CountOfColumn - 1) rank--;
                }
            }
            return rank;
        }
    }

    public Matrix Reverse => (1 / Determinant) * Transpose(GetTheMatrixOfAlgebraicComplement(this));
    public double[,] Body { get; }
    public int CountOfRow { get; }
    public int CountOfColumn { get; }
}