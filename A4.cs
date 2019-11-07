diff --git a/A4/A4.cs b/A4/A4.cs
index a5949a5..00b8614 100644
--- a/A4/A4.cs
+++ b/A4/A4.cs
@@ -11,95 +11,181 @@ namespace COMP10066_Lab4
      * for sample standard deviation calculations
      *
      * @author Joey Programmer
+     * 
+     * Modefied by: Peter Babb
+     *      student number: 000793674
+     *      date: November 10 2019
      */
 
+    /**
+        do use PascalCasing for class names and method names.
+
+        do use camelCasing for method arguments and local variables.
+
+        do not use Hungarian notation or any other type identification in identifiers
+
+        do not use Screaming Caps for constants or readonly variables
+
+        avoid using Abbreviations. Exceptions: abbreviations commonly used as names, such as Id, Xml, Ftp, Uri
+
+        do use predefined type names instead of system type names like Int16, Single, UInt64, etc
+
+        do use noun or noun phrases to name a class.
+
+        do name source files according to their main classes. Exception: file names with partial classes
+          reflect their source or purpose, e.g. designer, generated, etc.
+        
+        do vertically align curly brackets.
+    */
+
+
+
     public class A4
+{
+    /// <summary>
+    /// 
+    /// Goes through the given list and counts the total number of positive numbers OR
+    /// positive and negative numbers.
+    /// Calculates the average of all the values.
+    /// 
+    /// </summary>
+    /// 
+    /// <param name="data">List of doubles to operate on</param>
+    /// <param name="includeNegatives">boolean to dictate whether negative numbers are included in calculations</param>
+    /// 
+    /// <returns>The average of all valid values in double list </returns>
+    public static double Average(List<double> data, bool includeNegatives)
     {
-        public static double Avg(List<double> x, bool incneg)
+        int count = 0;
+        if (includeNegatives) //Counts all items if includeNegatives is set to True
+            count = data.Count;
+        else
         {
-            double s = Sum(x, incneg);
-            int c = 0;
-            for (int i = 0; i < x.Count; i++)
-            {
-                if (incneg || x[i] >= 0)
-                {
-                    c++;
-                }
-            }
-            if (c == 0)
+            foreach(double value in data) //Goes through each item in the list
             {
-                throw new ArgumentException("no values are > 0");
+                if (value >= 0) //Only counts the positive items
+                    count++;
             }
-            return s / c;
         }
 
-        public static double Sum(List<double> x, bool incneg)
+        if (count == 0)//Checks for division by 0
         {
-            if (x.Count < 0)
-            {
-                throw new ArgumentException("x cannot be empty");
-            }
+            throw new ArgumentException("List is either empty or" +
+                " attempting to exclude negative values in data with no positive values");
+        }
+        return Sum(data, includeNegatives) / count;//returns the average value of the list
+    }
 
-            double sum = 0.0;
-            foreach (double val in x)
-            {
-                if (incneg || val >= 0)
-                {
-                    sum += val;
-                }
-            }
-            return sum;
+    /// <summary>
+    /// 
+    /// Checks for an empty list
+    /// Calculates to total of all the values in a list of doubles
+    /// Can include or exclude negative numbers.
+    /// 
+    /// </summary>
+    /// 
+    /// <param name="data">List of doubles to operate on</param>
+    /// <param name="includeNegatives">Determines whether negative numbers are included</param>
+    /// 
+    /// <returns>Total of all values</returns>
+    public static double Sum(List<double> data, bool includeNegatives)
+    {
+        if (data.Count <= 0)//check for empty list
+        {
+            throw new ArgumentException("Cannot operate on an empty list");
         }
 
-        public static double Median(List<double> data)
+        double sum = 0.0;//initialize sum
+        foreach (double value in data)//goes through list
         {
-            if (data.Count == 0)
+            if (includeNegatives || value >= 0) //checks whether to add negative values and adds all positive values
             {
-                throw new ArgumentException("Size of array must be greater than 0");
+                sum += value;
             }
+        }
+        return sum; //returns total of all values
+    }
+    /// <summary>
+    /// 
+    /// Checks if list is empty.
+    /// Sorts the list.
+    /// Determines median based on the length of the list.
+    /// 
+    /// </summary>
+    /// 
+    /// <param name="data">List of doubles to operate on</param>
+    /// 
+    /// <returns>Middle value of list</returns>
+    public static double Median(List<double> data)
+    {
+        if (data.Count == 0)//checks for empty list
+        {
+            throw new ArgumentException("Cannot operate on an empty list");
+        }
 
-            data.Sort();
+        data.Sort();//sorts by value, ascending
 
-            double median = data[data.Count / 2];
-            if (data.Count % 2 == 0)
-                median = (data[data.Count / 2] + data[data.Count / 2 - 1]) / 2;
+        double median = data[data.Count / 2];
 
-            return median;
-        }
+        if (data.Count % 2 == 0)// if length of data is even, median must be average of the two middle values
+            median = (data[data.Count / 2] + data[data.Count / 2 - 1]) / 2;
+
+        return median; //returns middle value of list
+    }
 
-        public static double SD(List<double> data)
+    /// <summary>
+    /// 
+    /// Checks that list of double has at least two values.
+    /// Calculates sum of all values minus the average then squared.
+    /// Calculates the square root of the sum of differences squared divided by the size of the sample minus 1.
+    /// 
+    /// </summary>
+    /// 
+    /// <param name="data">List of doubles to operate on</param>
+    /// 
+    /// <returns>Sample standard deviation of data set</returns>
+    public static double StandardDeviation(List<double> data)
+    {
+        if (data.Count <= 1)//Checks that the size of the list is at least 2
         {
-            if (data.Count <= 1)
-            {
-                throw new ArgumentException("Size of array must be greater than 1");
-            }
+            throw new ArgumentException("Size of array must be greater than 1");
+        }
 
-            int n = data.Count;
-            double s = 0;
-            double a = Avg(data, true);
+        int numberOfSamples = data.Count;//initializes variables
+        double sumOfDifferencesSquared = 0;
+        double average = Average(data, true);
 
-            for (int i = 0; i < n; i++)
-            {
-                double v = data[i];
-                s += Math.Pow(v - a, 2);
-            }
-            double stdev = Math.Sqrt(s / (n - 1));
-            return stdev;
+        foreach(double value in data)//Calculates sum of all values minus the average then squared.
+        {
+            sumOfDifferencesSquared += Math.Pow(value - average, 2);
         }
+        
+        return Math.Sqrt(sumOfDifferencesSquared / (numberOfSamples - 1));
+        //returns the square root of the sum of differences squared divided by the size of the sample minus 1
+    }
 
-        // Simple set of tests that confirm that functions operate correctly
-        private static void Main(string[] args)
+    // Simple set of tests that confirm that functions operate correctly
+    private static void Main(string[] args)
+    {
+        List<double> testData = new List<double> { 2.2, 3.3, 66.2, 17.5, 30.2, 31.1 };
+        
+        try
         {
-            List<Double> testDataD = new List<Double> { 2.2, 3.3, 66.2, 17.5, 30.2, 31.1 };
-
-            Console.WriteLine("The sum of the array = {0}", Sum(testDataD, true));
+            Console.WriteLine("The sum of the data set = {0:f3}", Sum(testData, true));
 
-            Console.WriteLine("The average of the array = {0}", Avg(testDataD, true));
+            Console.WriteLine("The average of the data set = {0:f3}", Average(testData, true));
 
-            Console.WriteLine("The median value of the Double data set = {0}", Median(testDataD));
+            Console.WriteLine("The median value of the data set = {0:f3}", Median(testData));
 
-            Console.WriteLine("The sample standard deviation of the Double test set = {0}\n", SD(testDataD));
-            Console.Read();
+            Console.WriteLine("The sample standard deviation of the data set = {0:f3}\n", StandardDeviation(testData));
+            
+        }
+        catch (Exception e)//catches any errors with the data
+        {
+            Console.WriteLine(e.Message);
         }
+        Console.Read();
     }
-}
\ No newline at end of file
+}
+}
+ 
\ No newline at end of file
