#include "Core.hpp"

int main(int argc, char const *argv[])
{
  Core core;

	if (argc > 1)
		core.exec(argv[1]);
  return 0;
}
