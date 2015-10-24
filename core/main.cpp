#include "DOS.hpp"

int main(int argc, char const *argv[])
{
  DOS dos;

  dos.call("rm");

  return 0;
}
