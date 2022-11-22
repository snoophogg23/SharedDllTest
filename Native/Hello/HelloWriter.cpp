#include "HelloWriter.h"

using namespace Hello;
using namespace std;

void globalHello() {
  cout << "Hello, global world!\n";
}
  
namespace Hello {
  void namespaceHello() {
    cout << "Hello, namespace world!\n";
  }
  void HelloWriter::staticHello() {
    cout << "Hello, static world!\n";
  }
  void HelloWriter::sayHello() {
    cout << "Hello, world!\n";
  }
}