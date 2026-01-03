using System;
using System.Collections.Generic;
using System.Text;

namespace NFCTagReader.Shared.Models;

public class NfcTag
{
    public string? SerialNumber { get; set; }

    public IEnumerable<NfcRecord>? Records { get; set; }
}
