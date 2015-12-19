#include "tcp_server.hpp"
#include "tcp_connection.hpp"

int	main(int ac, char *av[])
{
  try
    {
      boost::asio::io_service io_service;
      tcp_server server(io_service, 7171);
      io_service.run();
    }
  catch (std::exception& e)
    {
      std::cerr << e.what() << std::endl;
    }
  return 0;
}
