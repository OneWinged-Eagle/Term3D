#include "tcp_connection.hpp"

tcp_connection::tcp_connection(boost::asio::io_service& io_service)
  : m_socket(io_service)
{}

tcp_connection::~tcp_connection()
{}

boost::asio::ip::tcp::socket&	tcp_connection::socket()
{
  return m_socket;
}

std::string	tcp_connection::make_daytime_string()
{
  using namespace std;
  time_t now = time(0);
  return ctime(&now);
}

void	tcp_connection::start()
{
  m_message = "Connected on server   " + make_daytime_string();

  boost::asio::async_write(m_socket, boost::asio::buffer(m_message), boost::bind(&tcp_connection::handle_write, shared_from_this(), boost::asio::placeholders::error));
}

void	tcp_connection::handle_write(const boost::system::error_code& error)
{
  if (!error)
    {
    }
}

tcp_connection::pointer		tcp_connection::create(boost::asio::io_service& io_service)
{
  return pointer(new tcp_connection(io_service));
}
