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
using NUnit.Framework.Constraints;

namespace ModelingTools.Tests.DateSequence
{

    /// <summary>
    /// Tests PFunction2D Interpolations.
    /// </summary>
    [TestFixture]
    public class TestDateSequence
    {
        static double Tolerance = 1e-8;
        /// <summary>
        /// Initializes the backend to run the tests.
        /// </summary>
        [SetUp]
        public void Init()
        {
            TestCommon.TestInitialization.CommonInitialization();
            
        }



        [TestCase(DateFrequency.NoFrequency, 0.0)]
        [TestCase(DateFrequency.Daily, 1.0 / 365)]
        [TestCase(DateFrequency.Weekly, 1.0 / 52)]
        [TestCase(DateFrequency.BiWeekly, 2.0 / 52)]
        [TestCase(DateFrequency.Monthly, 1.0 / 12)]
        [TestCase(DateFrequency.Quarterly, 3.0 / 12)]
        [TestCase(DateFrequency.ThreePerAnnum, 4.0 / 12)]
        [TestCase(DateFrequency.Semiannual, 0.5)]
        [TestCase(DateFrequency.Annual, 1.0)]
        [TestCase(DateFrequency.EveryTwoYears, 2.0)]
        [TestCase(DateFrequency.EveryThreeYears, 3.0)]
        public void DateFrequency2YearFraction_Parametrized(DateFrequency frequency, double expected)
        {
            var actual = Freq2Period.DateFrequency2YearFraction(frequency);
            Assert.That(actual, Is.EqualTo(expected).Within(Tolerance));
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

        /// <summary>
        /// Verifies that a sequence referencing another vector gets that vector's dates without the skipped
        /// ones, whatever the order in which the two sequences were added to the project.
        /// </summary>
        [TestCase(true)]
        [TestCase(false)]
        public void TestVectorReference_ParsedAfterReferencedSequence(bool referencedAddedFirst)
        {
            ProjectROV prj = CreateVectorReferenceProject(referencedAddedFirst, autocallStart: 4);

            prj.Parse();

            Assert.That(prj.HasErrors, Is.False);
            Assert.That(GetDates(prj, ReferencingName), Is.EqualTo(MonthlyDates(2020, firstMonth: 4)));
        }

        /// <summary>
        /// Verifies that the skip of a vector reference is taken from the current value of its expression,
        /// not from the one of the previous parse.
        /// </summary>
        [Test]
        public void TestVectorReference_SkipPeriodsArrayUsesCurrentValue()
        {
            ProjectROV prj = CreateVectorReferenceProject(referencedAddedFirst: true, autocallStart: 4);
            prj.Parse();

            ModelParameter autocallStart = prj.GetModelParameter(["AutocallStart"]);
            autocallStart.m_Value = (RightValue)2.0;
            prj.Parse();

            Assert.That(GetDates(prj, ReferencingName), Is.EqualTo(MonthlyDates(2020, firstMonth: 2)));
        }

        /// <summary>
        /// Verifies that the expressions used to order the symbols include the vector reference and its skip.
        /// </summary>
        [Test]
        public void TestTransformExpressions_IncludeVectorReferenceAndSkip()
        {
            ModelParameterDateSequence ds = new("StrikeDate", "LastObservation", "FrequencyCoupon")
            {
                SkipPeriods = new ModelParameter("PlusStart"),
                VectorReferenceExpr = "@odPlus",
                SkipPeriodsArray = new ModelParameter("AutocallStart-1"),
            };

            Assert.That(ds.TransformExpressions, Is.SupersetOf(new[]
            {
                "StrikeDate", "LastObservation", "FrequencyCoupon", "PlusStart", "@odPlus", "AutocallStart-1",
            }));
        }

        private const string ReferencedName = "odPlus";
        private const string ReferencingName = "odFreq";

        /// <summary>
        /// Creates a project with a monthly sequence for 2020 and a sequence referencing it that skips
        /// <c>AutocallStart-1</c> dates.
        /// </summary>
        /// <remarks>
        /// The referencing sequence has its own (NoFrequency) dates: they are used when the reference cannot be
        /// resolved, so a wrong parse order shows up as a wrong result.
        /// </remarks>
        private static ProjectROV CreateVectorReferenceProject(bool referencedAddedFirst, double autocallStart)
        {
            Document d = new();
            ProjectROV prj = new(d);
            d.Part.Add(prj);

            ModelParameterDateSequence referenced = new(new DateTime(2020, 1, 1), new DateTime(2020, 12, 1), DateFrequency.Monthly)
            {
                Name = ReferencedName,
                Description = ReferencedName,
            };

            ModelParameterDateSequence referencing = new(new DateTime(2020, 1, 1), new DateTime(2020, 1, 10), DateFrequency.NoFrequency)
            {
                Name = ReferencingName,
                Description = ReferencingName,
                VectorReferenceExpr = "@" + ReferencedName,
                SkipPeriodsArray = new ModelParameter("AutocallStart-1"),
            };

            prj.Symbols.Add(new ModelParameter(autocallStart, "", "AutocallStart"));
            if (referencedAddedFirst)
            {
                prj.Symbols.Add(referenced);
                prj.Symbols.Add(referencing);
            }
            else
            {
                prj.Symbols.Add(referencing);
                prj.Symbols.Add(referenced);
            }

            return prj;
        }

        private static IEnumerable<DateTime> GetDates(ProjectROV prj, string name)
        {
            ModelParameterDateSequence sequence = (ModelParameterDateSequence)prj.GetModelParameter([name]);
            return sequence.Values.Select(x => ((RightValueDate)x).m_Date.Date);
        }

        private static IEnumerable<DateTime> MonthlyDates(int year, int firstMonth) =>
            Enumerable.Range(firstMonth, 12 - firstMonth + 1).Select(month => new DateTime(year, month, 1));


    }
}
