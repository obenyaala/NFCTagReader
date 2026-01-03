using System;
using System.Collections.Generic;
using System.Text;

namespace NFCTagReader.Shared.Models;

public class NfcRecord
{
    public string? Message { get; set; }
    public NfcTypeFormat TypeFormat { get; set; }
    public string? MimeType { get; set; }
}

public enum NfcTypeFormat
{
    Empty = 0x00,
    WellKnown = 0x01,
    Mime = 0x02,
    Uri = 0x03,
    External = 0x04,
    Unknown = 0x05,
    Unchanged = 0x06,
    Reserved = 0x07
}