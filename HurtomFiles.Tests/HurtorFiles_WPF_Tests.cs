using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using NUnit.Framework;

namespace HurtomFiles.Tests
{
    [TestFixture]
    public class HurtorFiles_WPF_Tests
    {
        public const string exePath = @"E:\visual studio\Projects\HurtomFiles\HurtomFiles\HurtomFiles.WPF\bin\Debug\netcoreapp3.1\HurtomFiles.WPF.exe";
        [Test]
        public void HurtomFiles_WPF_Start() 
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = exePath;
            p.Start();
            p.WaitForExit();
            //throw new Exception(Assembly.GetExecutingAssembly().Location);
        }
    }
}
