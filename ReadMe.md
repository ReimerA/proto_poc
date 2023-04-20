The first part of program.cs shows how to generate the .proto file from our probuf-net decorated classes. You will notie the output refers to a bcl proto file. this file can be found at https://github.com/protobuf-net/protobuf-net/blob/main/src/Tools/bcl.proto
This file is the one that has the types like datetime and decimal that does not match any default types in protobuf, and therefore are modelled in a particular way. THIS IS THE PROBLEM/CHALLENGE.

I have placed a modified output with only the needed types removing the reference to the bcl file, and adding a protoc option of a namespace - all of this only for easy of use. From this a generated file for google.protobuf library can be generated like this


protoc.exe --csharp_out=.\Google\ .\protobuf-net\generated_modified.proto

Download the protoc tool here https://github.com/protocolbuffers/protobuf/releases/tag/v22.3

You will then see that data serialized with protobuf-net can be deserialized with google.protobuf library fine, as long as we mind the types used. I.e., we cannot assume that the special types used in one library will be defined the same using another library.

If we for example assume google.proto timestamp kind defined here https://github.com/protocolbuffers/protobuf/blob/main/src/google/protobuf/timestamp.proto, we see that here it is defined in seconds and nanoseconds, where as in the protobuf-net bcl we are talking for example ticks, and can also define more detail.