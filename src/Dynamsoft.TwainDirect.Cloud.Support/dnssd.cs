///////////////////////////////////////////////////////////////////////////////////////
//
// TwainDirect.Support.Dnssd
//
// What we have here is a class that lets us register or monitor for _twaindirect
// subtypes under _privet._tcp.  The code is currently for Bonjour on Windows.
// Adding Avahi for Linux and Bonjour for Mac shouldn't be too big of a deal, but
// it'll have to come later...
//
// As for the mDNS / DNS-SD advertising, we're complying with the TWAIN Local
// Specification:  https://twaindirect.org
//
///////////////////////////////////////////////////////////////////////////////////////
//  Author          Date            Comment
//  M.McLaughlin    01-Jul-2015     Initial Release
///////////////////////////////////////////////////////////////////////////////////////
//  Copyright (C) 2016-2018 Kodak Alaris Inc.
//
//  Permission is hereby granted, free of charge, to any person obtaining a
//  copy of this software and associated documentation files (the "Software"),
//  to deal in the Software without restriction, including without limitation
//  the rights to use, copy, modify, merge, publish, distribute, sublicense,
//  and/or sell copies of the Software, and to permit persons to whom the
//  Software is furnished to do so, subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
//  THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
//  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
//  DEALINGS IN THE SOFTWARE.
///////////////////////////////////////////////////////////////////////////////////////

// Helpers...
using System;

// Namespace for things shared across the system...
namespace Dynamsoft.TwainDirect.Cloud.Support.Dnssd
{

    /// <summary>
    /// Data gleaned from zeroconf about a device, the fields are
    /// organized in the order we'd like to see them sorted, but
    /// the CompareTo function takes care of the actual comparisons...
    /// </summary>
    public class DnssdDeviceInfo : IComparable
    {
        // Public Methods
        #region Public Methods

        /// <summary>
        /// Init stuff...
        /// </summary>
        public DnssdDeviceInfo()
        {
            m_szTxtTy = "";
            m_szServiceName = "";
            m_szLinkLocal = "";
            m_lInterface = 0;
            m_szIpv4 = "";
            m_szIpv6 = "";
            m_lPort = 0;
            m_lFlags = 0;
            m_lTtl = 0;
            m_szTxtTxtvers = "";
            m_szTxtType = "";
            m_szTxtId = "";
            m_szTxtTy = "";
            m_szTxtCs = "";
            m_szTxtNote = "";
            m_blTxtHttps = false;
            m_aszTxt = null;
            m_blIsCloud = false;
        }

        /// <summary>
        /// We need this so we can clone the object...
        /// </summary>
        /// <param name="a_dnssddeviceinfo">the beastie to copy</param>
        public DnssdDeviceInfo(DnssdDeviceInfo a_dnssddeviceinfo)
        {
            m_szTxtTy = a_dnssddeviceinfo.m_szTxtTy;
            m_szServiceName = a_dnssddeviceinfo.m_szServiceName;
            m_szLinkLocal = a_dnssddeviceinfo.m_szLinkLocal;
            m_lInterface = a_dnssddeviceinfo.m_lInterface;
            m_szIpv4 = a_dnssddeviceinfo.m_szIpv4;
            m_szIpv6 = a_dnssddeviceinfo.m_szIpv6;
            m_lPort = a_dnssddeviceinfo.m_lPort;
            m_lFlags = a_dnssddeviceinfo.m_lFlags;
            m_lTtl = a_dnssddeviceinfo.m_lTtl;
            m_szTxtTxtvers = a_dnssddeviceinfo.m_szTxtTxtvers;
            m_szTxtType = a_dnssddeviceinfo.m_szTxtType;
            m_szTxtId = a_dnssddeviceinfo.m_szTxtId;
            m_szTxtTy = a_dnssddeviceinfo.m_szTxtTy;
            m_szTxtCs = a_dnssddeviceinfo.m_szTxtCs;
            m_szTxtNote = a_dnssddeviceinfo.m_szTxtNote;
            m_blTxtHttps = a_dnssddeviceinfo.m_blTxtHttps;
            m_blIsCloud = a_dnssddeviceinfo.m_blIsCloud;

            // Handle the weird one...
            if ((a_dnssddeviceinfo.m_aszTxt == null) || (a_dnssddeviceinfo.m_aszTxt.Length == 0))
            {
                m_aszTxt = null;
            }
            else
            {
                int ii;
                m_aszTxt = new string[a_dnssddeviceinfo.m_aszTxt.Length];
                for (ii = 0; ii < a_dnssddeviceinfo.m_aszTxt.Length; ii++)
                {
                    m_aszTxt[ii] = a_dnssddeviceinfo.m_aszTxt[ii];
                }
            }
        }

        /// <summary>
        /// Add a text record to the TXT array...
        /// </summary>
        /// <param name="a_szTxt">record to add</param>
        public void AddTxt(string a_szTxt)
        {
            if (m_aszTxt == null)
            {
                m_aszTxt = new string[1];
                m_aszTxt[0] = a_szTxt;
            }
            else
            {
                string[] asz = new string[m_aszTxt.Length + 1];
                Array.Copy(m_aszTxt, asz, m_aszTxt.Length);
                asz[m_aszTxt.Length] = a_szTxt;
                m_aszTxt = asz;
            }
        }

        /// <summary>
        /// Implement our IComparable interface...
        /// </summary>
        /// <param name="obj">the object to compare against</param>
        /// <returns>-1, 0, 1</returns>
        public int CompareTo(object obj)
        {
            // Well, that's odd...
            if (obj == null)
            {
                return (0);
            }

            // This is our object...
            if (obj is DnssdDeviceInfo)
            {
                int iResult;
                iResult = this.m_szTxtTy.CompareTo(((DnssdDeviceInfo)obj).m_szTxtTy);
                if (iResult == 0)
                {
                    iResult = this.m_szServiceName.CompareTo(((DnssdDeviceInfo)obj).m_szServiceName);
                }
                if (iResult == 0)
                {
                    iResult = this.m_szLinkLocal.CompareTo(((DnssdDeviceInfo)obj).m_szLinkLocal);
                }
                if (iResult == 0)
                {
                    iResult = this.m_lInterface.CompareTo(((DnssdDeviceInfo)obj).m_lInterface);
                }
                return (iResult);
            }

            // No joy...
            return (0);
        }

        /// <summary>
        /// Get the flags reported with it...
        /// </summary>
        /// <returns>flags reported with it</returns>
        public long GetFlags()
        {
            return (m_lFlags);
        }

        /// <summary>
        /// Get the interface it lives on...
        /// </summary>
        /// <returns>interface it lives on</returns>
        public long GetInterface()
        {
            return (m_lInterface);
        }

        /// <summary>
        /// Get the IPv4 address (if any)...
        /// </summary>
        /// <returns>IPv4 address (if any)</returns>
        public string GetIpv4()
        {
            return (m_szIpv4);
        }

        /// <summary>
        /// Get the IPv6 address (if any)...
        /// </summary>
        /// <returns>IPv6 address (if any)</returns>
        public string GetIpv6()
        {
            return (m_szIpv6);
        }

        /// <summary>
        /// Get the link local name for where the service is running...
        /// </summary>
        /// <returns>link local name for where the service is running</returns>
        public string GetLinkLocal()
        {
            return (m_szLinkLocal);
        }

        /// <summary>
        /// Get the port to use...
        /// </summary>
        /// <returns>port to use</returns>
        public long GetPort()
        {
            return (m_lPort);
        }

        /// <summary>
        /// Get the full, unique name of the device...
        /// </summary>
        /// <returns>unique name of the device</returns>
        public string GetServiceName()
        {
            return (m_szServiceName);
        }

        /// <summary>
        /// Get our time to live...
        /// </summary>
        /// <returns>time to live</returns>
        public long GetTtl()
        {
            return (m_lTtl);
        }

        /// <summary>
        /// Get the array of TXT records...
        /// </summary>
        /// <returns>array of TXT records</returns>
        public string[] GetTxt()
        {
            int ii;
            string[] aszTxt;

            // No data...
            if ((m_aszTxt == null) || (m_aszTxt.Length == 0))
            {
                return (null);
            }

            // Make a copy...
            aszTxt = new string[m_aszTxt.Length];
            for (ii = 0; ii < m_aszTxt.Length; ii++)
            {
                aszTxt[ii] = m_aszTxt[ii];
            }
            return (aszTxt);
        }

        /// <summary>
        /// Get our cloud status...
        /// </summary>
        /// <returns>cloud status</returns>
        public string GetTxtCs()
        {
            return (m_szTxtCs);
        }

        /// <summary>
        /// Get the HTTPS flag...
        /// </summary>
        /// <returns>https flag</returns>
        public bool GetTxtHttps()
        {
            return (m_blTxtHttps);
        }

        /// <summary>
        /// Get our text id, cloud id, empty if one isn't available...
        /// </summary>
        /// <returns>text id, cloud id, empty if one isn't available</returns>
        public string GetTxtId()
        {
            return (m_szTxtId);
        }

        /// <summary>
        /// Get a note about the device...
        /// </summary>
        /// <returns>a note about the device</returns>
        public string GetTxtNote()
        {
            return (m_szTxtNote);
        }

        /// <summary>
        /// Get our TXT version...
        /// </summary>
        /// <returns>our TXT version</returns>
        public string GetTxtTxtvers()
        {
            return (m_szTxtTxtvers);
        }

        /// <summary>
        /// Get the friendly name for the scanner...
        /// </summary>
        /// <returns>friendly name for the scanner</returns>
        public string GetTxtTy()
        {
            return (m_szTxtTy);
        }

        /// <summary>
        /// Get the text type, comma separated services supported by the device...
        /// </summary>
        /// <returns>text type, comma separated services supported by the device</returns>
        public string GetTxtType()
        {
            return (m_szTxtType);
        }

        /// <summary>
        /// True if the selected item is cloud-based, else local...
        /// </summary>
        /// <returns>true if cloud</returns>
        public bool IsCloud()
        {
            return (m_blIsCloud);
        }

        /// <summary>
        /// Set true if this is a cloud connection...
        /// </summary>
        /// <param name="a_blIsCloud">true if a cloud connection</param>
        public void SetCloud(bool a_blIsCloud)
        {
            m_blIsCloud = a_blIsCloud;
        }

        /// <summary>
        /// Set the flags reported with it...
        /// </summary>
        /// <param name="a_lFlags">flags reported with it</param>
        public void SetFlags(long a_lFlags)
        {
            m_lFlags = a_lFlags;
        }

        /// <summary>
        /// Set the interface it lives on...
        /// </summary>
        /// <param name="a_lInterface">interface it lives on</param>
        public void SetInterface(long a_lInterface)
        {
            m_lInterface = a_lInterface;
        }

        /// <summary>
        /// Set the IPv4 address (if any)...
        /// </summary>
        /// <param name="a_szIpv4">IPv4 address (if any)</param>
        public void SetIpv4(string a_szIpv4)
        {
            m_szIpv4 = a_szIpv4;
        }

        /// <summary>
        /// Set the IPv6 address (if any)...
        /// </summary>
        /// <param name="a_szIpv6">IPv6 address (if any)</param>
        public void SetIpv6(string a_szIpv6)
        {
            m_szIpv6 = a_szIpv6;
        }

        /// <summary>
        /// Set link local name for where the service is running...
        /// </summary>
        /// <param name="a_szLinkLocal">link local name for where the service is running</param>
        public void SetLinkLocal(string a_szLinkLocal)
        {
            m_szLinkLocal = a_szLinkLocal;
        }

        /// <summary>
        /// Set the port to use...
        /// </summary>
        /// <param name="a_lPort">port to use</param>
        public void SetPort(long a_lPort)
        {
            m_lPort = a_lPort;
        }

        /// <summary>
        /// Set the full, unique name of the device...
        /// </summary>
        /// <param name="a_szServiceName">unique name of the device</param>
        public void SetServiceName(string a_szServiceName)
        {
            m_szServiceName = a_szServiceName;
        }

        /// <summary>
        /// Set our time to live...
        /// </summary>
        /// <param name="a_lTtl">time to live</param>
        public void SetTtl(long a_lTtl)
        {
            m_lTtl = a_lTtl;
        }

        /// <summary>
        /// Set the array of TXT records...
        /// </summary>
        /// <param name="a_aszTxt">set the array of TXT records</param>
        public void SetTxt(string[] a_aszTxt)
        {
            int ii;

            // No data...
            if ((a_aszTxt == null) || (a_aszTxt.Length == 0))
            {
                m_aszTxt = null;
                return;
            }

            // Make a copy...
            m_aszTxt = new string[a_aszTxt.Length];
            for (ii = 0; ii < a_aszTxt.Length; ii++)
            {
                m_aszTxt[ii] = a_aszTxt[ii];
            }
        }

        /// <summary>
        /// Set our cloud status...
        /// </summary>
        /// <param name="a_szTxtCs">cloud status</param>
        public void SetTxtCs(string a_szTxtCs)
        {
            m_szTxtCs = a_szTxtCs;
        }

        /// <summary>
        /// Set the HTTPS flag...
        /// </summary>
        /// <param name="a_blTxtHttps">https flag</param>
        public void SetTxtHttps(bool a_blTxtHttps)
        {
            m_blTxtHttps = a_blTxtHttps;
        }

        /// <summary>
        /// Set our text id, cloud id, empty if one isn't available...
        /// </summary>
        /// <param name="a_szTxtId">text id, cloud id, empty if one isn't available</param>
        public void SetTxtId(string a_szTxtId)
        {
            m_szTxtId = a_szTxtId;
        }

        /// <summary>
        /// Set a note about the device...
        /// </summary>
        /// <param name="a_szTxtNote">a note about the device</param>
        public void SetTxtNote(string a_szTxtNote)
        {
            m_szTxtNote = a_szTxtNote;
        }

        /// <summary>
        /// Set our TXT verison...
        /// </summary>
        /// <param name="a_szTxtTxtvers">our TXT version</param>
        public void SetTxtTxtvers(string a_szTxtTxtvers)
        {
            m_szTxtTxtvers = a_szTxtTxtvers;
        }

        /// <summary>
        /// Set the friendly name for the scanner...
        /// </summary>
        /// <param name="a_szTxtTy">friendly name for the scanner</param>
        public void SetTxtTy(string a_szTxtTy)
        {
            m_szTxtTy = a_szTxtTy;
        }

        /// <summary>
        /// Set the text type, comma separated services supported by the device...
        /// </summary>
        /// <param name="a_szTxtType">text type, comma separated services supported by the device</param>
        public void SetTxtType(string a_szTxtType)
        {
            m_szTxtType = a_szTxtType;
        }

        #endregion


        // Private Attributes
        #region Private Attributes

        /// <summary>
        /// The flags reported with it...
        /// </summary>
        private long m_lFlags;

        /// <summary>
        /// The interface it lives on...
        /// </summary>
        private long m_lInterface;

        /// <summary>
        /// The IPv4 address (if any)...
        /// </summary>
        private string m_szIpv4;

        /// <summary>
        /// The IPv6 address (if any)...
        /// </summary>
        private string m_szIpv6;

        /// <summary>
        /// True if this is a cloud connection...
        /// </summary>
        private bool m_blIsCloud;

        /// <summary>
        /// The link local name for where the service is running...
        /// </summary>
        private string m_szLinkLocal;

        /// <summary>
        /// The port to use...
        /// </summary>
        private long m_lPort;

        /// <summary>
        /// The full, unique name of the device...
        /// </summary>
        private string m_szServiceName;

        /// <summary>
        /// Our time to live...
        /// </summary>
        private long m_lTtl;

        /// <summary>
        /// The array of TXT records...
        /// </summary>
        private string[] m_aszTxt;

        /// <summary>
        /// Text cs, cloud status...
        /// </summary>
        private string m_szTxtCs;

        /// <summary>
        /// true if the scanner wants us to use HTTPS...
        /// </summary>
        private bool m_blTxtHttps;

        /// <summary>
        /// Text id, cloud id, empty if one isn't available...
        /// </summary>
        private string m_szTxtId;

        /// <summary>
        /// Text note, optional, a note about the device, like its location...
        /// </summary>
        private string m_szTxtNote;

        /// <summary>
        /// Our TXT version...
        /// </summary>
        private string m_szTxtTxtvers;

        /// <summary>
        /// Text ty, friendly name for the scanner...
        /// </summary>
        private string m_szTxtTy;

        /// <summary>
        /// Text type, comma separated services supported by the device...
        /// </summary>
        private string m_szTxtType;

        #endregion
    }

}
