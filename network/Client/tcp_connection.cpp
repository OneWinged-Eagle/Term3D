#include "tcp_connection.hpp"

tcp_connection::tcp_connection(boost::asio::io_service& io_service)
  :m_socket(io_service)
{}

tcp_connection::~tcp_connection()
{}

tcp_connection::pointer		tcp_connection::create(boost::asio::io_service& io_service)
{
  return pointer(new tcp_connection(io_service));
}

void				tcp_connection::read()
{
  boost::asio::async_read(m_socket, boost::asio::buffer(m_network_buffer),
			  boost::asio::transfer_at_least(20),
			  boost::bind(&tcp_connection::handle_read, shared_from_this(),
				      boost::asio::placeholders::error,
				      boost::asio::placeholders::bytes_transferred)
			  );
}

boost::asio::ip::tcp::socket&	tcp_connection::socket()
{
  return m_socket;
}

void				tcp_connection::handle_read(const boost::system::error_code& error, size_t number_bytes_read)
{
  if (!error)
    {
      std::cout.write(&m_network_buffer[0], number_bytes_read);
      read();
    }
  else
    std::cout << error.message() << std::endl;
}
