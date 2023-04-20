

using ProtoBuf;

//Generate .proto file from type
//  string proto = Serializer.GetProto<DataChangedPubMsg>();
//  File.WriteAllText("protobuf-net/generated.proto", proto);


	var msgObj = new DataChangedPubMsg() { DateTime=DateTime.Now, Amount=123.456M};
	Console.WriteLine($"object for protobuf-net serialization: {msgObj.DateTime.Ticks}");

	byte[] msgbytes;
	using (var ms = new MemoryStream())
	{
		Serializer.Serialize(ms, msgObj);
		msgbytes = ms.ToArray();
	}
	
	var outMsg = Google.Protobuf.TestProto.DataChangedPubMsg.Parser.ParseFrom(msgbytes);
    Console.WriteLine($"Google Deserialized ticks value: {outMsg.DateTime.Value}");
    Console.WriteLine($"Google Deserialized Decimal Hi value: {outMsg.Amount.Hi}");
    Console.WriteLine($"Google Deserialized Decimal Lo value: {outMsg.Amount.Lo}");

	
	//outMsg.Dump();
