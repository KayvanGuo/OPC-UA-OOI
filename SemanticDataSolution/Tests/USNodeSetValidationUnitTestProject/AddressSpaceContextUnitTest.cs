﻿
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UAOOI.SemanticData.UANodeSetValidation.UAInformationModel;
using UAOOI.SemanticData.UANodeSetValidation.UnitTest.Helpers;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UANodeSetValidation.UnitTest
{
  [TestClass]
  public class AddressSpaceContextUnitTest
  {

    [TestMethod]
    [TestCategory("Code")]
    public void AddressSpaceContextCreatorTestMethod()
    {
      AddressSpaceContext _as = new AddressSpaceContext(x => { });
      Assert.IsNotNull(_as);
      Assert.IsNotNull(_as.UTTryGetUANodeContext(VariableTypes.PropertyType));
    }
    [TestMethod]
    [TestCategory("Code")]
    [ExpectedException(typeof(ArgumentNullException))]
    public void AddressSpaceContextImportUANodeSetNullTestMethod1()
    {
      IAddressSpaceContext _as = new AddressSpaceContext(x => { });
      Assert.IsNotNull(_as);
      UANodeSet _ns = null;
      _as.ImportUANodeSet(_ns);
    }
    [TestMethod]
    [TestCategory("Code")]
    [ExpectedException(typeof(ArgumentNullException))]
    public void AddressSpaceContextImportUANodeSetNullTestMethod2()
    {
      IAddressSpaceContext _as = new AddressSpaceContext(x => { });
      Assert.IsNotNull(_as);
      FileInfo _fi = null;
      _as.ImportUANodeSet(_fi);
    }
    [TestMethod]
    [TestCategory("Code")]
    [ExpectedException(typeof(FileNotFoundException))]
    public void AddressSpaceContextNotExistingFileNameTestMethod()
    {
      IAddressSpaceContext _as = new AddressSpaceContext(x => { });
      Assert.IsNotNull(_as);
      FileInfo _fi = new FileInfo("NotExistingFileName.xml");
      _as.ImportUANodeSet(_fi);
    }
    [TestMethod]
    [TestCategory("Code")]
    public void AddressSpaceContextValidateAndExportModelTestMethod1()
    {
      UANodeSet _ns;
      IAddressSpaceContext _as;
      ValidateAndExportModelPreparation(out _ns, out _as);
      _as.ValidateAndExportModel(_ns.NamespaceUris[0]);
    }
    [TestMethod]
    [TestCategory("Code")]
    public void AddressSpaceContextValidateAndExportModelTestMethod2()
    {
      UANodeSet _ns;
      IAddressSpaceContext _as;
      ValidateAndExportModelPreparation(out _ns, out _as);
      _as.ValidateAndExportModel();
    }
    [TestMethod]
    [TestCategory("Code")]
    [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
    public void AddressSpaceContextValidateAndExportModelTestMethod3()
    {
      UANodeSet _ns;
      IAddressSpaceContext _as;
      ValidateAndExportModelPreparation(out _ns, out _as);
      _as.ValidateAndExportModel("Not existing namespace");
    }
    [TestMethod]
    [TestCategory("Code")]
    public void AddressSpaceContextValidateAndExportModelTestMethod4()
    {
      UANodeSet _ns;
      IAddressSpaceContext _as;
      ValidateAndExportModelPreparation(out _ns, out _as);
      Assert.AreEqual<int>(1514, ((AddressSpaceContext)_as).UTValidateAndExportModel(0).Count);
      Assert.AreEqual<int>(1, ((AddressSpaceContext)_as).UTValidateAndExportModel(1).Count);
    }

    #region private
    //Helpers
    private static void ValidateAndExportModelPreparation(out UANodeSet _ns, out IAddressSpaceContext _as)
    {
      _ns = TestData.CreateNodeSetModel();
      List<TraceMessage> _trace = new List<TraceMessage>();
      int _diagnosticCounter = 0;
      Assert.AreEqual<int>(0, _trace.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
      Assert.IsTrue(_ns.NamespaceUris.Length >= 1, "Wrong test data - NamespaceUris must contain more then 2 items");
      _as = new AddressSpaceContext(x => { Helpers.TraceHelper.TraceDiagnostic(x, _trace, ref _diagnosticCounter); });
      Assert.IsNotNull(_as);
      Assert.AreEqual<int>(0, _trace.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
      _as.ImportUANodeSet(_ns);
      Assert.AreEqual<int>(0, _trace.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
    }
    #endregion

  }
}
