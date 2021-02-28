const PROTO_PATH = __dirname + '/greet.proto';

const grpc = require('grpc');
const protoLoader = require('@grpc/proto-loader');

let packageDefinition = protoLoader.loadSync(
    PROTO_PATH,
    {keepCase: true,
     longs: String,
     enums: String,
     defaults: true,
     oneofs: true
    });
let greet_proto = grpc.loadPackageDefinition(packageDefinition).greet;

function main() {
  let client = new greet_proto.Greeter('127.0.0.1:4500',
                                       grpc.credentials.createInsecure());
    client.sayHello({name: 'you'}, function(err, response) {
    console.log('Greeting:', response.message);
    });
}

main();