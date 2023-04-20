// You can define other methods, fields, classes and namespaces here

using ProtoBuf;

[ProtoContract]
public class DataChangedPubMsg
{
	private IDictionary<string, string> m_values;
	private bool _needsWritingToRedis = false;
	private const string TimestampKey = "_sampletimestamp";

	[ProtoMember(1)]
	public string TurbineId { get; set; }

	[ProtoMember(2)]
	public DateTime DateTime { get; set; }

	[ProtoMember(3)]
	public IDictionary<string, string> Values
	{
		get { return m_values ?? (m_values = new Dictionary<string, string>()); }
		set { m_values = value; }
	}

	/// <summary>
	/// Flag to signal that value has not yet been written to Redis, and some downstream service picking this of of Kafka should do that.
	/// Added as part of the MEC/CtCN project, where data arriving from the DataRelay needs to end up in EDA (Kafka/Redis) like it would have had it been pulled by the Reapers instead.
	/// The Reapers always write to both Kafka and Redis, therefore this value will be false for data added by a Reaper, but for DataRelay data this flag can be used to separate the 
	/// jobs of writing to Kafka and writing to Redis between different services.
	/// </summary>
	[ProtoMember(4, IsRequired = false)]
	public virtual bool NeedsWritingToRedis { get { return _needsWritingToRedis; } set { _needsWritingToRedis = value; } }

    
	[ProtoMember(5)]
	public Decimal Amount { get; set; }
}