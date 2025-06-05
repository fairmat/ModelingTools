using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatesGenerator;
using DVPLDOM;
using DVPLI;
using NUnit.Framework.Legacy;

namespace ModelingTools.Tests.DateSequence
{

    /// <summary>
    /// Tests PFunction2D Interpolations.
    /// </summary>
    [TestFixture]
    public class TestDateSequence
    {
        /// <summary>
        /// Initializes the backend to run the tests.
        /// </summary>
        [SetUp]
        public void Init()
        {
            TestCommon.TestInitialization.CommonInitialization();
        }


        [Test]
        public void TestNoFreq()
        {
            var d = new Document();
            var prj = new ProjectROV(d);
            d.Part.Add(prj); 

            var startDate = new DateTime(2020, 1, 1);
            var endDate = new DateTime(2020, 1, 10);
            var frequency = DateFrequency.NoFrequency;
            int skip = 0;

            var ds = new ModelParameterDateSequence(startDate, endDate, frequency);
            ds.Name = "MyDateSeq";
            ds.Description = ds.Name;

            ds.SkipPeriods = skip;

            prj.Symbols.Add(ds);
            prj.Parse();

            var resulting = prj.GetModelParameter(["MyDateSeq"]);
            var resultingSequence = resulting as ModelParameterDateSequence;

            var resultingDates = resultingSequence.Values.Select(x => ((RightValueDate)x).m_Date);
            var expectedDates = new DateTime[] {startDate, endDate };

            Assert.That(resultingDates, Is.EqualTo(expectedDates));
        }

        [Test]
        public void TestNoFreq_Backward()
        {
            var d = new Document();
            var prj = new ProjectROV(d);
            d.Part.Add(prj);

            var startDate = new DateTime(2020, 1, 1);
            var endDate = new DateTime(2020, 1, 10);
            var frequency = DateFrequency.NoFrequency;
            int skip = 0;

            var ds = new ModelParameterDateSequence(startDate, endDate, frequency);
            ds.Name = "MyDateSeq";
            ds.Description = ds.Name;
            ds.GenerateSequenceFromStartDate = false;
            ds.SkipPeriods = skip;

            prj.Symbols.Add(ds);
            prj.Parse();

            var resulting = prj.GetModelParameter(["MyDateSeq"]);
            var resultingSequence = resulting as ModelParameterDateSequence;

            var resultingDates = resultingSequence.Values.Select(x => ((RightValueDate)x).m_Date);
            var expectedDates = new DateTime[] { startDate, endDate };

            Assert.That(resultingDates, Is.EqualTo(expectedDates));
        }


        [Test]
        public void TestNoFreq_Skip1()
        {
            var d = new Document();
            var prj = new ProjectROV(d);
            d.Part.Add(prj);

            var startDate = new DateTime(2020, 1, 1);
            var endDate = new DateTime(2020, 1, 10);
            var frequency = DateFrequency.NoFrequency;
            int skip = 1;

            var ds = new ModelParameterDateSequence(startDate, endDate, frequency);
            ds.Name = "MyDateSeq";
            ds.Description = ds.Name;

            ds.SkipPeriods = skip;

            prj.Symbols.Add(ds);
            prj.Parse();

            var resulting = prj.GetModelParameter(["MyDateSeq"]);
            var resultingSequence = resulting as ModelParameterDateSequence;

            var resultingDates = resultingSequence.Values.Select(x => ((RightValueDate)x).m_Date);
            var expectedDates = new DateTime[] { endDate };

            Assert.That(resultingDates, Is.EqualTo(expectedDates));
        }


        [Test]
        public void TestNoFreq_Skip5()
        {
            var d = new Document();
            var prj = new ProjectROV(d);
            d.Part.Add(prj);

            var startDate = new DateTime(2020, 1, 1);
            var endDate = new DateTime(2020, 1, 10);
            var frequency = DateFrequency.NoFrequency;
            int skip = 5;

            var ds = new ModelParameterDateSequence(startDate, endDate, frequency);
            ds.Name = "MyDateSeq";
            ds.Description = ds.Name;

            ds.SkipPeriods = skip;

            prj.Symbols.Add(ds);
            prj.Parse();

            var resulting = prj.GetModelParameter(["MyDateSeq"]);
            var resultingSequence = resulting as ModelParameterDateSequence;

            var resultingDates = resultingSequence.Values.Select(x => ((RightValueDate)x).m_Date);
            var expectedDates = new DateTime[] {  };

            Assert.That(resultingDates, Is.EqualTo(expectedDates));
        }


    }
}
