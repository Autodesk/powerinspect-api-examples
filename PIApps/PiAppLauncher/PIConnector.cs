// **********************************************************************
// *         © COPYRIGHT 2019 Autodesk, Inc.All Rights Reserved         *
// *                                                                    *
// *  Use of this software is subject to the terms of the Autodesk      *
// *  license agreement provided at the time of installation            *
// *  or download, or which otherwise accompanies this software         *
// *  in either electronic or hard copy form.                           *
// **********************************************************************

using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Delcam.PowerINSPECT.Libraries.AddInsUtils;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using PWICOMMANDBARSLib;

namespace PiAppLauncher
{

    [ComVisible(true)]
    public class PIConnector : PIConnectorCustomBar

    {

        /// <summary>
        ///     class to store data of Pi APP to start
        /// </summary>
        private class ClsPiAppProcess
        {
            public Boolean Active { get; set; }
            public string Title { get; set; }
            public string ProcessPath { get; set; }
            public string Info { get; set; }
        }
        private readonly List<ClsPiAppProcess> _piAppProcesses = new List<ClsPiAppProcess>();

        private XmlDocument _xDoc;

        /// <summary>
        /// constructor
        /// </summary>
        public PIConnector()
            : base()
        {
            m_instance = this;
        }

        /// <summary>
        /// destructor
        /// </summary>
        ~PIConnector()
        {
            m_instance = null;
        }

        /// <summary>
        /// instance of PI connector
        /// </summary>
        internal static PIConnector instance
        {
            get { return m_instance; }
        }


        /// <summary>
        /// Name of command bar
        /// </summary>
        protected override string addin_bar_name
        {
            get
            {
                return "PI APPS";
            }
        }


        /// <summary>
        /// position of command bar
        /// </summary>
        protected override PWICommandBarPosition command_bar_position
        {
            get
            {
                return PWICommandBarPosition.pwiBarBottom;
            }
        }


        /// <summary>
        ///     Reads data of PmApp configuration file. Gets macro data to be executed
        /// </summary>
        private void ReadPiAppProcessConfig()
        {
            _piAppProcesses.Clear();
            string filename = 
                Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) +
                Path.DirectorySeparatorChar + "PiAppConfig.xml";
            if (File.Exists(filename))
            {
                _xDoc = new XmlDocument();
                _xDoc.Load(filename);

                if (_xDoc.DocumentElement != null)
                {
                    // anlyse definition data dependent on previously read parse type
                    foreach (XmlNode xBaseNode in _xDoc.DocumentElement.ChildNodes)
                    {
                        // PIAPPSPROCESSES
                        // analyse structure, look for cild nodes respectively the components/elements of the PiAppsProcesses
                        if ((xBaseNode != null) && (xBaseNode.Name == "PiAppsProcesses"))
                        {
                            // add PiAppsProcess definition to list of PiAppsProcesses
                            foreach (XmlNode xNode in xBaseNode.ChildNodes)
                            {
                                if ((xNode != null) && (xNode.Name == "PiAppsProcess"))
                                {
                                    if (xNode.Attributes != null)
                                    {
                                        var aPiAppProcess = new ClsPiAppProcess()
                                        {
                                            Active =
                                                xNode.Attributes.GetNamedItem("Active") != null && 
                                                    Convert.ToBoolean(xNode.Attributes.GetNamedItem("Active").Value),
                                            Title =
                                                xNode.Attributes.GetNamedItem("Title") == null
                                                    ? string.Empty
                                                    : xNode.Attributes.GetNamedItem("Title").Value,
                                            ProcessPath =
                                                xNode.Attributes.GetNamedItem("ProcessPath") == null
                                                    ? string.Empty
                                                    : xNode.Attributes.GetNamedItem("ProcessPath").Value,
                                            Info =
                                                xNode.Attributes.GetNamedItem("Info") == null
                                                    ? string.Empty
                                                    : xNode.Attributes.GetNamedItem("Info").Value
                                        };
                                        if (aPiAppProcess.Active)
                                        {
                                            _piAppProcesses.Add(aPiAppProcess);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Add buttons to current command bar
        /// </summary>
        protected override void CreateCommandBarButtons()
        {
            // add first button to toolbar, button defined by application (hard coded)
            AddCommandBarButton(
              "PI DocModifier",
              startApp_PiDocModifier,
              "Start PI DocModifier",
              0 // image #0 from AddInButton.png (first image from strip)
            );

            AddCommandBarButton(
              "PI CopyTransform",
              startApp_PiCopyTransform,
              "Start PI CopyTransform",
              1 // image #1 from AddInButton.png (second image from strip)
            );

            AddCommandBarButton(
              "PI Miscellaneous",
              startApp_PiMiscellaneous,
              "Start PI Miscellaneous",
              2 // image #2 from AddInButton.png (third image from strip)
            );

            #region unusedCommandBarButtons
           // AddCommandBarButton(
           //   "My button3",
           //   MyButtonHandler3,
           //   "Briefly about button\nVery long description about button for status line",
           //   3 // image #0 from AddInButton.png (first image from strip)
           //);
           // AddCommandBarButton(
           //   "My button4",
           //   MyButtonHandler4,
           //   "Briefly about button\nVery long description about button for status line",
           //   4 // image #0 from AddInButton.png (first image from strip)
           //);
           // AddCommandBarButton(
           //   "My button5",
           //   MyButtonHandler5,
           //   "Briefly about button\nVery long description about button for status line",
           //   5 // image #0 from AddInButton.png (first image from strip)
           //);
           // AddCommandBarButton(
           //   "My button6",
           //   MyButtonHandler6,
           //   "Briefly about button\nVery long description about button for status line",
           //   6 // image #0 from AddInButton.png (first image from strip)
           //);
           // AddCommandBarButton(
           //   "My button7",
           //   MyButtonHandler7,
           //   "Briefly about button\nVery long description about button for status line",
           //   7 // image #0 from AddInButton.png (first image from strip)
           //);
           // AddCommandBarButton(
           //   "My button8",
           //   MyButtonHandler8,
           //   "Briefly about button\nVery long description about button for status line",
           //   8 // image #0 from AddInButton.png (first image from strip)
           //);
           // AddCommandBarButton(
           //   "My button9",
           //   MyButtonHandler9,
           //   "Briefly about button\nVery long description about button for status line",
           //   9 // image #0 from AddInButton.png (first image from strip)
           //);

	#endregion

            // add buttons, dfined by data read out of PiAppConfig.xml
            ReadPiAppProcessConfig();
            // add up to 3 buttons to command bar, which are configurable by XML
            for (int i = 0; i < _piAppProcesses.Count; i++)
            {
                if (File.Exists(_piAppProcesses[i].ProcessPath))
                {
                    switch (i)
                    {
                        case 0:
                            AddCommandBarButton(
                                _piAppProcesses[i].Title,
                                start_PiAppProcess0,
                                _piAppProcesses[i].Info,
                                9 // image #9 from AddInButton.png 
                            );
                            break;
                        case 1:
                            AddCommandBarButton(
                              _piAppProcesses[i].Title,
                              start_PiAppProcess1,
                              _piAppProcesses[i].Info,
                              10 // image #10 from AddInButton.png 
                            );
                            break;
                        case 2:
                            AddCommandBarButton(
                              _piAppProcesses[i].Title,
                              start_PiAppProcess2,
                              _piAppProcesses[i].Info,
                              11 // image #11 from AddInButton.png 
                            );
                            break;
                    }
                }
            }

        }


        /// <summary>
        /// starts application PI doc modifier (stand alone app /EXE)
        /// </summary>
        public void startApp_PiDocModifier()
        {
            string piDocModPath =
                Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) +
                Path.DirectorySeparatorChar + "PI DocModifier" +
                Path.DirectorySeparatorChar + "PI DocModifier.exe";
            if (File.Exists(piDocModPath))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = piDocModPath;
                startInfo.Arguments = string.Empty;
                startInfo.WindowStyle = ProcessWindowStyle.Normal;
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = false;
                Process process = Process.Start(startInfo);
                if (process != null)
                {
                    // process running, anything elese to do?
                }
                else
                {
                    MessageBox.Show("_Unable to start PI DocModifier", "_Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        /// <summary>
        /// starts application PI CopyTransform (stand alone app /EXE)
        /// </summary>
        public void startApp_PiCopyTransform()
        {
            string piCopyTransformPath =
                Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) +
                Path.DirectorySeparatorChar + "PI CopyTransform" +
                Path.DirectorySeparatorChar + "PI CopyTransform.exe";
            if (File.Exists(piCopyTransformPath))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = piCopyTransformPath;
                startInfo.Arguments = string.Empty;
                startInfo.WindowStyle = ProcessWindowStyle.Normal;
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = false;
                Process process = Process.Start(startInfo);
                if (process != null)
                {
                    // process running, anything elese to do?
                }
                else
                {
                    MessageBox.Show("_Unable to start PI CopyTransform", "_Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        /// <summary>
        /// starts application PI Miscellaneous (stand alone app /EXE)
        /// </summary>
        public void startApp_PiMiscellaneous()
        {
            string piMiscellaneousPath =
                Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) +
                Path.DirectorySeparatorChar + "PI Miscellaneous" +
                Path.DirectorySeparatorChar + "PI Miscellaneous.exe";
            if (File.Exists(piMiscellaneousPath))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = piMiscellaneousPath;
                startInfo.Arguments = string.Empty;
                startInfo.WindowStyle = ProcessWindowStyle.Normal;
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = false;
                Process process = Process.Start(startInfo);
                if (process != null)
                {
                    // process running, anything elese to do?
                }
                else
                {
                    MessageBox.Show("_Unable to start PI Miscellaneous", "_Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        public void MyButtonHandler1()
        {
            MessageBox.Show("Button1"); 
        }
        public void MyButtonHandler2()
        {
            MessageBox.Show("Button2");
        }
        public void MyButtonHandler3()
        {
            MessageBox.Show("Button3");
        }
        public void MyButtonHandler4()
        {
            MessageBox.Show("Button4");
        }
        public void MyButtonHandler5()
        {
            MessageBox.Show("Button5");
        }
        public void MyButtonHandler6()
        {
            MessageBox.Show("Button6");
        }
        public void MyButtonHandler7()
        {
            MessageBox.Show("Button7");
        }
        public void MyButtonHandler8()
        {
            MessageBox.Show("Button8");
        }
        public void MyButtonHandler9()
        {
            MessageBox.Show("Button9");
        }

        /// <summary>
        /// starts process of first item read out of config file PiAppConfig.xml
        /// </summary>
        public void start_PiAppProcess0()
        {
            start_PiAppProcess(_piAppProcesses[0].ProcessPath);
        }
        /// <summary>
        /// starts process of second item read out of config file PiAppConfig.xml
        /// </summary>
        public void start_PiAppProcess1()
        {
            start_PiAppProcess(_piAppProcesses[1].ProcessPath);
        }
        /// <summary>
        /// starts process of third item read out of config file PiAppConfig.xml
        /// </summary>
        public void start_PiAppProcess2()
        {
            start_PiAppProcess(_piAppProcesses[2].ProcessPath);
        }

        /// <summary>
        /// starts process read out of config file PiAppConfig.xml
        /// </summary>
        public void start_PiAppProcess(string processFilename)
        {
            if (File.Exists( processFilename))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = processFilename;
                startInfo.Arguments = string.Empty;
                startInfo.WindowStyle = ProcessWindowStyle.Normal;
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = false;
                Process process = Process.Start(startInfo);
                if (process != null)
                {
                    // process running, anything elese to do?
                }
                else
                {
                    MessageBox.Show("_Unable to start process.", "_Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

       

        private static PIConnector m_instance;



       
    } 

} 

