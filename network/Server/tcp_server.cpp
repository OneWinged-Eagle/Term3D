#include "tcp_server.hpp"
#include "tcp_connection.hpp"

tcp_server::tcp_server(boost::asio::io_service& io_service, int port)
  : m_acceptor(io_service, boost::asio::ip::tcp::endpoint(boost::asio::ip::tcp::v4(), port))
{
  start_accept();
}

tcp_server::~tcp_server()
{
}

void	tcp_server::start_accept()
{
  tcp_connection::pointer new_connection = tcp_connection::create(m_acceptor.get_io_service());
  m_acceptor.async_accept(new_connection->socket(), boost::bind(&tcp_server::handle_accept, this, new_connection, boost::asio::placeholders::error));
}

void	tcp_server::handle_accept(tcp_connection::pointer new_connection, const boost::system::error_code& error)
{
  if (!error)
    {
      std::cout << "Reçu un client !" << std::endl;
      new_connection->start();
      start_accept();
    }
}
