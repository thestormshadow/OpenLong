using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Long.Kernel.Utils
{
	public class MemoryCompressor
	{
		private static readonly ILogger logger = Log.ForContext<MemoryCompressor>();
		internal class Native
		{
			[DllImport("kernel32")]
			public static extern IntPtr OpenProcess(UInt32 dwAccess, Int32 dwDesiredAccess, UInt32 dwPID);
			[DllImport("psapi")]
			public static extern Int32 EmptyWorkingSet(IntPtr dwObject);
			[DllImport("kernel32")]
			public static extern Int32 CloseHandle(IntPtr dwObject);
		}

		private uint m_ProcessId = 0;
		public MemoryCompressor()
		{
			m_ProcessId = (uint)Process.GetCurrentProcess().Id;
		}
		public void Optimize()
		{
			if (m_ProcessId != 0)
			{
				IntPtr Handle = Native.OpenProcess((uint)0x1F0FFF, 1, m_ProcessId);
				Native.EmptyWorkingSet(Handle);
				Native.CloseHandle(Handle);
				logger.Information("MemoryCompressor Optimize is Ready");
			}
			else
				throw new Exception("MeomoryCompressor::Optimize() -> The process Id can't be equal to zero!");
		}
		public void Close()
		{
			m_ProcessId = 0;
		}
	}
}
