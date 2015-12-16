#include "tcp_client.hpp"
#include "tcp_connection.hpp"

int	main(int ac, char *av[])
{
  try
    {
      boost::asio::io_service io_service;
      boost::asio::ip::tcp::endpoint endpoint(boost::asio::ip::address::from_string("127.0.0.1"), 7171);
      tcp_client client(io_service, endpoint);
      io_service.run();
    }
  catch(std::exception& e)
    {
      std::cerr << e.what() << std::endl;
    }
  return 0;
}
