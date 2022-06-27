namespace salty.core.Util
{
    using System;

   /// <summary>
   /// RandomProvider.  Provides random numbers of all data types
   /// in specified ranges.  It also contains a couple of methods
   /// from Normally (Gaussian) distributed random numbers and 
   /// Exponentially distributed random numbers.
   /// </summary>
   public static class RandomProvider
   {
      private static Random _mRng1;
      private static double _mStoredUniformDeviate;
      private static bool _mStoredUniformDeviateIsGood = false;

      #region -- Construction/Initialization --

      static RandomProvider()
      {
         _mRng1 = new Random(Environment.TickCount);
      }
      public static void Reset()
      {
         _mRng1 = new Random(Environment.TickCount);
      }

      #endregion

      #region -- Uniform Deviates --

      /// <summary>
      /// Returns double in the range [0, 1)
      /// </summary>
      public static double Next()
      {
         return _mRng1.NextDouble();
      }

      /// <summary>
      /// Returns true or false randomly.
      /// </summary>
      public static bool NextBoolean()
      {
         return _mRng1.Next(0,2) != 0;
      }

      /// <summary>
      /// Returns double in the range [0, 1)
      /// </summary>
      public static double NextDouble()
      {
         var rn = _mRng1.NextDouble();
         return rn;
      }

      /// <summary>
      /// Returns Int16 in the range [min, max)
      /// </summary>
      public static short Next(short min, short max)
      {
         if (max <= min) 
         {
            string message = "Max must be greater than min.";
            throw new ArgumentException(message);
         }
         var rn = (max*1.0 - min*1.0)*_mRng1.NextDouble() + min*1.0;
         return Convert.ToInt16(rn);
      }

      /// <summary>
      /// Returns Int32 in the range [min, max)
      /// </summary>
      public static int Next(int min, int max)
      {
         return _mRng1.Next(min, max);
      }

      /// <summary>
      /// Returns Int64 in the range [min, max)
      /// </summary>
      public static long Next(long min, long max)
      {
         if (max <= min) 
         {
            string message = "Max must be greater than min.";
            throw new ArgumentException(message);
         }

         var rn = (max*1.0 - min*1.0)*_mRng1.NextDouble() + min*1.0;
         return Convert.ToInt64(rn);
      }

      /// <summary>
      /// Returns float (Single) in the range [min, max)
      /// </summary>
      public static float Next(float min, float max)
      {
         if (max <= min) 
         {
            string message = "Max must be greater than min.";
            throw new ArgumentException(message);
         }

         var rn = (max*1.0 - min*1.0)*_mRng1.NextDouble() + min*1.0;
         return Convert.ToSingle(rn);
      }

      /// <summary>
      /// Returns double in the range [min, max)
      /// </summary>
      public static double Next(double min, double max)
      {
         if (max <= min) 
         {
            string message = "Max must be greater than min.";
            throw new ArgumentException(message);
         }

         var rn = (max - min)*_mRng1.NextDouble() + min;
         return rn;
      }

      /// <summary>
      /// Returns DateTime in the range [min, max)
      /// </summary>
      public static DateTime Next(DateTime min, DateTime max)
      {
         if (max <= min) 
         {
            string message = "Max must be greater than min.";
            throw new ArgumentException(message);
         }
         var minTicks = min.Ticks;
         var maxTicks = max.Ticks;
         var rn = (Convert.ToDouble(maxTicks) 
                   - Convert.ToDouble(minTicks))*_mRng1.NextDouble() 
                  + Convert.ToDouble(minTicks);
         return new DateTime(Convert.ToInt64(rn));
      }

      /// <summary>
      /// Returns TimeSpan in the range [min, max)
      /// </summary>
      public static TimeSpan Next(TimeSpan min, TimeSpan max)
      {
         if (max <= min)
         {
            const string message = "Max must be greater than min.";
            throw new ArgumentException(message);
         }

         var minTicks = min.Ticks;
         var maxTicks = max.Ticks;
         var rn = (Convert.ToDouble(maxTicks) 
                   - Convert.ToDouble(minTicks))*_mRng1.NextDouble() 
                  + Convert.ToDouble(minTicks);
         return new TimeSpan(Convert.ToInt64(rn));
      }

      /// <summary>
      /// Returns double in the range [min, max)
      /// </summary>
      public static double NextUniform()
      {
         return Next();
      }

      /// <summary>
      /// Returns a uniformly random integer representing one of the values 
      /// in the enum.
      /// </summary>
      public static int NextEnum(Type enumType)
      {
         int[] values = (int[])Enum.GetValues(enumType);
         var randomIndex = Next(0, values.Length);
         return values[randomIndex];
      }

      #endregion

      #region -- Exponential Deviates --

      /// <summary>
      /// Returns an exponentially distributed, positive, random deviate 
      /// of unit mean.
      /// </summary>
      public static double NextExponential()
      {
         var dum = 0.0;
         while (dum == 0.0)
            dum=NextUniform();
         return -1.0*Math.Log(dum, Math.E);
      }

      #endregion

      #region -- Normal Deviates --

      /// <summary>
      /// Returns a normally distributed deviate with zero mean and unit 
      /// variance.
      /// </summary>
      public static double NextNormal()
      {
         // based on algorithm from Numerical Recipes
         if (_mStoredUniformDeviateIsGood)
         {
            _mStoredUniformDeviateIsGood = false;
            return _mStoredUniformDeviate;
         }
         var rsq = 0.0;
         double v1 = 0.0, v2 = 0.0, fac = 0.0;
         while (rsq >=1.0 || rsq == 0.0)
         {
            v1 = 2.0*Next() - 1.0;
            v2 = 2.0*Next() - 1.0;
            rsq = v1*v1 + v2*v2;
         }
         fac = Math.Sqrt(-2.0 *Math.Log(rsq, Math.E)/rsq);
         _mStoredUniformDeviate = v1*fac;
         _mStoredUniformDeviateIsGood = true;
         return v2*fac;
      }

      #endregion

   }
}