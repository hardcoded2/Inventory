// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: tile_data.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from tile_data.proto</summary>
public static partial class TileDataReflection {

  #region Descriptor
  /// <summary>File descriptor for tile_data.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static TileDataReflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "Cg90aWxlX2RhdGEucHJvdG8i1AEKCFRpbGVEYXRhEg4KBlRpbGVJRBgBIAEo",
          "BRIdChVUaWxlSHVtYW5SZWFkYWJsZU5hbWUYAiABKAkSMgoNQ29sbGlzaW9u",
          "VHlwZRgDIAEoDjIbLlRpbGVEYXRhLlRpbGVDb2xsaXNpb25UeXBlEhUKDXRp",
          "bGVJbWFnZURhdGEYBCABKAwSEQoJVGlsZVhTaXplGAUgASgFEhEKCVRpbGVZ",
          "U2l6ZRgGIAEoBSIoChFUaWxlQ29sbGlzaW9uVHlwZRIICgRMQU5EEAASCQoF",
          "V0FURVIQAWIGcHJvdG8z"));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { },
        new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
          new pbr::GeneratedClrTypeInfo(typeof(global::TileData), global::TileData.Parser, new[]{ "TileID", "TileHumanReadableName", "CollisionType", "TileImageData", "TileXSize", "TileYSize" }, null, new[]{ typeof(global::TileData.Types.TileCollisionType) }, null, null)
        }));
  }
  #endregion

}
#region Messages
public sealed partial class TileData : pb::IMessage<TileData> {
  private static readonly pb::MessageParser<TileData> _parser = new pb::MessageParser<TileData>(() => new TileData());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<TileData> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::TileDataReflection.Descriptor.MessageTypes[0]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public TileData() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public TileData(TileData other) : this() {
    tileID_ = other.tileID_;
    tileHumanReadableName_ = other.tileHumanReadableName_;
    collisionType_ = other.collisionType_;
    tileImageData_ = other.tileImageData_;
    tileXSize_ = other.tileXSize_;
    tileYSize_ = other.tileYSize_;
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public TileData Clone() {
    return new TileData(this);
  }

  /// <summary>Field number for the "TileID" field.</summary>
  public const int TileIDFieldNumber = 1;
  private int tileID_;
  /// <summary>
  ///for referential integrity
  /// </summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int TileID {
    get { return tileID_; }
    set {
      tileID_ = value;
    }
  }

  /// <summary>Field number for the "TileHumanReadableName" field.</summary>
  public const int TileHumanReadableNameFieldNumber = 2;
  private string tileHumanReadableName_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string TileHumanReadableName {
    get { return tileHumanReadableName_; }
    set {
      tileHumanReadableName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "CollisionType" field.</summary>
  public const int CollisionTypeFieldNumber = 3;
  private global::TileData.Types.TileCollisionType collisionType_ = global::TileData.Types.TileCollisionType.Land;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public global::TileData.Types.TileCollisionType CollisionType {
    get { return collisionType_; }
    set {
      collisionType_ = value;
    }
  }

  /// <summary>Field number for the "tileImageData" field.</summary>
  public const int TileImageDataFieldNumber = 4;
  private pb::ByteString tileImageData_ = pb::ByteString.Empty;
  /// <summary>
  ///Warning: max size 2^32
  /// </summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public pb::ByteString TileImageData {
    get { return tileImageData_; }
    set {
      tileImageData_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "TileXSize" field.</summary>
  public const int TileXSizeFieldNumber = 5;
  private int tileXSize_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int TileXSize {
    get { return tileXSize_; }
    set {
      tileXSize_ = value;
    }
  }

  /// <summary>Field number for the "TileYSize" field.</summary>
  public const int TileYSizeFieldNumber = 6;
  private int tileYSize_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int TileYSize {
    get { return tileYSize_; }
    set {
      tileYSize_ = value;
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as TileData);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(TileData other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (TileID != other.TileID) return false;
    if (TileHumanReadableName != other.TileHumanReadableName) return false;
    if (CollisionType != other.CollisionType) return false;
    if (TileImageData != other.TileImageData) return false;
    if (TileXSize != other.TileXSize) return false;
    if (TileYSize != other.TileYSize) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    if (TileID != 0) hash ^= TileID.GetHashCode();
    if (TileHumanReadableName.Length != 0) hash ^= TileHumanReadableName.GetHashCode();
    if (CollisionType != global::TileData.Types.TileCollisionType.Land) hash ^= CollisionType.GetHashCode();
    if (TileImageData.Length != 0) hash ^= TileImageData.GetHashCode();
    if (TileXSize != 0) hash ^= TileXSize.GetHashCode();
    if (TileYSize != 0) hash ^= TileYSize.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void WriteTo(pb::CodedOutputStream output) {
    if (TileID != 0) {
      output.WriteRawTag(8);
      output.WriteInt32(TileID);
    }
    if (TileHumanReadableName.Length != 0) {
      output.WriteRawTag(18);
      output.WriteString(TileHumanReadableName);
    }
    if (CollisionType != global::TileData.Types.TileCollisionType.Land) {
      output.WriteRawTag(24);
      output.WriteEnum((int) CollisionType);
    }
    if (TileImageData.Length != 0) {
      output.WriteRawTag(34);
      output.WriteBytes(TileImageData);
    }
    if (TileXSize != 0) {
      output.WriteRawTag(40);
      output.WriteInt32(TileXSize);
    }
    if (TileYSize != 0) {
      output.WriteRawTag(48);
      output.WriteInt32(TileYSize);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    if (TileID != 0) {
      size += 1 + pb::CodedOutputStream.ComputeInt32Size(TileID);
    }
    if (TileHumanReadableName.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(TileHumanReadableName);
    }
    if (CollisionType != global::TileData.Types.TileCollisionType.Land) {
      size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) CollisionType);
    }
    if (TileImageData.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeBytesSize(TileImageData);
    }
    if (TileXSize != 0) {
      size += 1 + pb::CodedOutputStream.ComputeInt32Size(TileXSize);
    }
    if (TileYSize != 0) {
      size += 1 + pb::CodedOutputStream.ComputeInt32Size(TileYSize);
    }
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(TileData other) {
    if (other == null) {
      return;
    }
    if (other.TileID != 0) {
      TileID = other.TileID;
    }
    if (other.TileHumanReadableName.Length != 0) {
      TileHumanReadableName = other.TileHumanReadableName;
    }
    if (other.CollisionType != global::TileData.Types.TileCollisionType.Land) {
      CollisionType = other.CollisionType;
    }
    if (other.TileImageData.Length != 0) {
      TileImageData = other.TileImageData;
    }
    if (other.TileXSize != 0) {
      TileXSize = other.TileXSize;
    }
    if (other.TileYSize != 0) {
      TileYSize = other.TileYSize;
    }
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(pb::CodedInputStream input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 8: {
          TileID = input.ReadInt32();
          break;
        }
        case 18: {
          TileHumanReadableName = input.ReadString();
          break;
        }
        case 24: {
          CollisionType = (global::TileData.Types.TileCollisionType) input.ReadEnum();
          break;
        }
        case 34: {
          TileImageData = input.ReadBytes();
          break;
        }
        case 40: {
          TileXSize = input.ReadInt32();
          break;
        }
        case 48: {
          TileYSize = input.ReadInt32();
          break;
        }
      }
    }
  }

  #region Nested types
  /// <summary>Container for nested types declared in the TileData message type.</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static partial class Types {
    public enum TileCollisionType {
      [pbr::OriginalName("LAND")] Land = 0,
      [pbr::OriginalName("WATER")] Water = 1,
    }

  }
  #endregion

}

#endregion


#endregion Designer generated code
