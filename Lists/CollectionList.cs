﻿using System;
using System.Collections;
using System.Threading;

namespace GenieClient.Genie.Collections
{
    public class CollectionList : CollectionBase
    {
        private ReaderWriterLock m_RWLock = new ReaderWriterLock();

        public bool AcquireWriterLock(int millisecondsTimeout = 0)
        {
            try
            {
                if (m_RWLock.IsWriterLockHeld | m_RWLock.IsReaderLockHeld)
                    return false;
                m_RWLock.AcquireWriterLock(millisecondsTimeout);
                return true;
            }
            #pragma warning disable CS0168
            catch (Exception ex)
            #pragma warning restore CS0168
            {
                return false;
            }
        }

        public bool AcquireReaderLock(int millisecondsTimeout = 0)
        {
            try
            {
                if (m_RWLock.IsWriterLockHeld)
                    return false;
                m_RWLock.AcquireReaderLock(millisecondsTimeout);
                return true;
            }
            #pragma warning disable CS0168
            catch (Exception ex)
            #pragma warning restore CS0168
            {
                return false;
            }
        }

        public LockCookie UpgradeToWriterLock(int millisecondsTimeout = 0)
        {
            try
            {
                if (m_RWLock.IsWriterLockHeld)
                    return default;
                return m_RWLock.UpgradeToWriterLock(millisecondsTimeout);
            }
            #pragma warning disable CS0168
            catch (Exception ex)
            #pragma warning restore CS0168
            {
                return default;
            }
        }

        public bool DowngradeToReaderLock(LockCookie cookie)
        {
            try
            {
                m_RWLock.DowngradeFromWriterLock(ref cookie);
                return true;
            }
            #pragma warning disable CS0168
            catch (Exception ex)
            #pragma warning restore CS0168
            {
                return default;
            }
        }

        public bool ReleaseWriterLock()
        {
            try
            {
                m_RWLock.ReleaseWriterLock();
                return true;
            }
            #pragma warning disable CS0168
            catch (Exception ex)
            #pragma warning restore CS0168
            {
                return false;
            }
        }

        public bool ReleaseReaderLock()
        {
            try
            {
                m_RWLock.ReleaseReaderLock();
                return true;
            }
            #pragma warning disable CS0168
            catch (Exception ex)
            #pragma warning restore CS0168
            {
                return false;
            }
        }
    }
}