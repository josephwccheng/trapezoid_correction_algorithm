using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Factorization;

namespace HomographicMatrix
{
    public class DLT // Direct Linear Transform
    {
        public static Matrix<double> CalculateHomographyMatrix(Quadrilateral srcQuadrilateral, Quadrilateral trgQuadrilateral)
        {
            double[,] squarePoints = srcQuadrilateral.convertToDouble();
            double[,] trapeziumPoints = trgQuadrilateral.convertToDouble();
            // Construct the linear system of equations
            int numPoints = squarePoints.GetLength(0);

            Matrix<double> A = DenseMatrix.Create(2 * numPoints, 9, 0.0);
            for (int i = 0; i < numPoints; i++)
            {
                double x = squarePoints[i, 0];
                double y = squarePoints[i, 1];
                double xPrime = trapeziumPoints[i, 0];
                double yPrime = trapeziumPoints[i, 1];

                A.SetRow(2 * i, new double[] { -x, -y, -1, 0, 0, 0, x * xPrime, y * xPrime, xPrime });
                A.SetRow(2 * i + 1, new double[] { 0, 0, 0, -x, -y, -1, x * yPrime, y * yPrime, yPrime });
            }

            // Perform Singular Value Decomposition (SVD) on matrix A
            Svd<double> svd = A.Svd(true);
            // Get the last column of the V matrix
            Vector<double> h = svd.VT.Row(svd.VT.RowCount - 1);
            // Normalize the homography matrix
            Matrix<double> homographicMatrix = DenseMatrix.OfArray(new double[,]
            {
                { h[0], h[1], h[2] },
                { h[3], h[4], h[5] },
                { h[6], h[7], h[8] }
            });
            return homographicMatrix;

        }
        public static Vector<double> Transform(Matrix<double> homographicMatrix, Vector<double> srcPoint)
        {
            // Append 1 to the end of source point to make it homogeneous
            Vector<double> srcPointHomogeneous = Vector<double>.Build.Dense(srcPoint.Count + 1);
            srcPointHomogeneous.SetSubVector(0, srcPoint.Count, srcPoint);
            srcPointHomogeneous[srcPoint.Count] = 1;
            // Perform matrix multiplication to obtain the target point in homogeneous coordinates
            Vector<double> targetPointHomogeneous = homographicMatrix * srcPointHomogeneous;
            Vector<double> targetPoint = targetPointHomogeneous / targetPointHomogeneous[targetPointHomogeneous.Count - 1];
            return targetPoint;
        }
    }
}







