﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.9.0.77
//      SpecFlow Generator Version:1.9.0.0
//      Runtime Version:4.0.30319.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace AllTheSame.WebAPI.Test.AcceptanceTests.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute()]
    public partial class CommunityTypeFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "CommunityType.feature"
#line hidden
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassInitializeAttribute()]
        public static void FeatureSetup(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext)
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "CommunityType", "In order to see a list of CommunityTypes\r\nAs a CommunityType Administrator\r\nI wan" +
                    "t to load a list of CommunityTypes", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassCleanupAttribute()]
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute()]
        public virtual void TestInitialize()
        {
            if (((TechTalk.SpecFlow.FeatureContext.Current != null) 
                        && (TechTalk.SpecFlow.FeatureContext.Current.FeatureInfo.Title != "CommunityType")))
            {
                AllTheSame.WebAPI.Test.AcceptanceTests.Features.CommunityTypeFeature.FeatureSetup(null);
            }
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCleanupAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("CommunityType--Add, Check, GetById, Update and Delete CommunityType")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "CommunityType")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("CommunityType")]
        public virtual void CommunityType_AddCheckGetByIdUpdateAndDeleteCommunityType()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("CommunityType--Add, Check, GetById, Update and Delete CommunityType", new string[] {
                        "CommunityType"});
#line 8
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Code",
                        "Label"});
            table1.AddRow(new string[] {
                        "SpecFlow",
                        "test"});
#line 9
 testRunner.Given("the following CommunityType Add input", ((string)(null)), table1, "Given ");
#line 12
 testRunner.When("I call the add CommunityType Post api endpoint to add a CommunityType it checks i" +
                    "f exists pulls item edits it and deletes it", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 13
 testRunner.Then("the add result should be a CommunityType Id check exists get by id edit and delet" +
                    "e with http response returns", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("CommunityType--Retrieve all CommunityTypes")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "CommunityType")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("CommunityTypes")]
        public virtual void CommunityType_RetrieveAllCommunityTypes()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("CommunityType--Retrieve all CommunityTypes", new string[] {
                        "CommunityTypes"});
#line 17
this.ScenarioSetup(scenarioInfo);
#line 19
 testRunner.When("I call the CommunityType Get api endpoint", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 20
 testRunner.Then("the get result should be a list of CommunityTypes", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Add a CommunityType")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "CommunityType")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("CommunityType")]
        public virtual void AddACommunityType()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Add a CommunityType", new string[] {
                        "CommunityType"});
#line 24
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Code",
                        "Label"});
            table2.AddRow(new string[] {
                        "SpecFlowTest",
                        "FlowTest"});
#line 25
 testRunner.Given("the following CommunityType Add input", ((string)(null)), table2, "Given ");
#line 28
 testRunner.When("I call the add CommunityType Post api endpoint to add a CommunityType", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 29
 testRunner.Then("the add result should be a CommunityType Id", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
