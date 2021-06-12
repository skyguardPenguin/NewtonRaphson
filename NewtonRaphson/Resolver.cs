using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octave.NET;
using Octave.NET.Tools;
namespace NewtonRaphson
{
    class Resolver
    {
        private float error=100;
        private float fxi;
        private float dxFxi;
        private float xiNext=0;

        public float acceptableError;
        public float xi;
        private string _function;
        public string function { get => _function; set { _function = value;} }
        public string dxFunction{ get =>dxFunction;set { dxFunction = value; } }

        public Function Func;
        public Resolver(float xi, float acceptableError,string function)
        {
            this.xi = xi;
            this.acceptableError = acceptableError;
            this.function = function;


            //Replace with your octave.bat path:
            Function.Path = @"C:\Users\sinoa\AppData\Local\Programs\GNU Octave\Octave-6.2.0\mingw64\bin\octave.bat";
            Func = new Function();

        }
        public Resolver()
        {
            //Replace with your octave.bat path:
            Function.Path = @"C:\Users\sinoa\AppData\Local\Programs\GNU Octave\Octave-6.2.0\mingw64\bin\octave.bat";
            Func = new Function();
            
        }

        public bool OctaveIsReady()
        {
            return Func.PackageLoaded();
        }
        public float Result()
        {
            error = 100;
            //fxi=0;
            //dxFxi=0;
            //xiNext = 0;
            int i=0;
            
           
            while (error>acceptableError)
            {
                if(i!=0)
                    xi = xiNext;
                fxi = (float)Func.Evaluate
                    (xi.ToString());
               
                dxFxi = (float)Func.Derivative(xi.ToString());
                xiNext =(float)xi-( (float)fxi / (float)dxFxi);
                if(i!=0)
                    error = (float)Math.Abs((xiNext - xi) / xiNext) * 100f;
                i++;
            }
        
            return xi;
        }
    }
}
