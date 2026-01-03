using NFCTagReader.Shared.Models;
using NFCTagReader.Shared.Services;
using Plugin.NFC;
using System;
using System.Collections.Generic;
using System.Text;

namespace NFCTagReader.Services;

public partial class NfcReaderWriter : INfcReaderWriter
{


    public event EventHandler<NfcTag> TagDiscoverded;

#if !ANDROID
    public bool IsAvailable { get; } = false;
    public bool IsEnabled { get; } = false;

    public Task WriteTagAsync(string message, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
#endif
}
