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

function sayHello(call, callback) {
    callback(null, {message: 'Hello ' + call.request.name});
  }

function main() {
  let server = new grpc.Server();
  server.addService(greet_proto.Greeter.service, {sayHello: sayHello});
  server.bind('0.0.0.0:4500', grpc.ServerCredentials.createInsecure());
  server.start();
}

main();