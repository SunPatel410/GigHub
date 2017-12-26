using NUnit.Framework;
using System;
using System.Transactions;


namespace GigHub.IntergrationTests
{
    public class Isolated : Attribute, ITestAction
    {
        private TransactionScope _transactionScope;
        public void BeforeTest(TestDetails testDetails)
        {
            _transactionScope = new TransactionScope();
        }

        public void AfterTest(TestDetails testDetails)
        {
            _transactionScope.Dispose();
        }

        //this method can only be applied to test methods
        public ActionTargets Targets
        {
            get { return ActionTargets.Test; }
        }
    }
}
