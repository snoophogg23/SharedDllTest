#include <HelloWriter.h>

using namespace std;
using namespace Hello;

int main(int argc, char *argv[]) {
  HelloWriter* writer = new HelloWriter();
  writer->sayHello();  
  HelloWriter::staticHello();
  Hello::namespaceHello();
  globalHello();
}