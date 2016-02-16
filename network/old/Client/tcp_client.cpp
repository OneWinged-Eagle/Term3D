#include "tcp_client.hpp"

tcp_client::tcp_client(boost::asio::io_service& io_service, boost::asio::ip::tcp::endpoint& endpoint)
  :m_io_service(io_service)
{
  connect(endpoint);
}

tcp_client::~tcp_client()
{}

void	tcp_client::connect(boost::asio::ip::tcp::endpoint& endpoint)
{
  tcp_connection::pointer new_connection = tcp_connection::create(m_io_service);
  boost::asio::ip::tcp::socket& socket = new_connection->socket();

  socket.async_connect(endpoint, boost::bind(&tcp_client::handle_connect, this, new_connection, boost::asio::placeholders::error));
}

void	tcp_client::handle_connect(tcp_connection::pointer new_connection, const boost::system::error_code& error)
{
  if (!error)
    {
      new_connection->read();
    }
}
