using NFCTagReader.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NFCTagReader.Shared.Services;

public interface INfcReaderWriter
{
    event EventHandler<NfcTag> TagDiscoverded;
    bool IsAvailable { get; }
    bool IsEnabled { get; }
    Task WriteTagAsync(string message, CancellationToken cancellationToken);
}
