# This is a sample Python script.

# Press Shift+F10 to execute it or replace it with your code.
# Press Double Shift to search everywhere for classes, files, tool windows, actions, and settings.
import grpc
import greet_pb2_grpc
import greet_pb2

def print_hi(name):
    # Use a breakpoint in the code line below to debug your script.
    print(f'Hi, {name}')  # Press Ctrl+F8 to toggle the breakpoint.
    channel = grpc.insecure_channel('localhost:4500')
    stub = greet_pb2_grpc.GreeterStub(channel)
    response = stub.SayHello(greet_pb2.HelloRequest(name='you'))
    print("Greeter client received: " + response.message)


# Press the green button in the gutter to run the script.
if __name__ == '__main__':
    print_hi('PyCharm')

# See PyCharm help at https://www.jetbrains.com/help/pycharm/
