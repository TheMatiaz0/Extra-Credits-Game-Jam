using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
#pragma warning disable
public class WaitForBackgroundThread
{
    public ConfiguredTaskAwaitable.ConfiguredTaskAwaiter GetAwaiter()
    {
        return Task.Run(() => {}).ConfigureAwait(false).GetAwaiter();
    }
}
