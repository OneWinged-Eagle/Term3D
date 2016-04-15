#include "Camera.h"

Camera::Camera()
	: m_phi(0.0), m_theta(0.0), m_orientation(), m_verticalAxe(0, 0, 1), m_lateralMove(), m_position(), m_sensib(0.0), m_speed(0.0)
{

}

Camera::Camera(glm::vec3 position, glm::vec3 targetPoint, glm::vec3 verticalAxe, float sensib, float speed)
	: m_phi(0.0), m_theta(0.0), m_orientation(), m_verticalAxe(verticalAxe), m_lateralMove(), m_position(position), m_sensib(sensib), m_speed(speed)
{

	//mise a jour du point visé
	setTargetPoint(targetPoint);
}

Camera::~Camera()
{

}




void	Camera::orient(int xRel, int YRel)
{
	//get des angles
	m_phi += -YRel * m_sensib;
	m_theta += -xRel * m_sensib;

	//limitation de phi
	if (m_phi > 89.0)
		m_phi = 89.0;
	else if (m_phi < -89.0)
		m_phi = -89.0;

	//conversion en rad oklm
	float phiRad = m_phi * M_PI / 180;
	float thetaRad = m_theta * M_PI / 180;

	//si vertical ax est x
	if (m_verticalAxe.x == 1.0)
	{
		//calcul des coords spheriques
		m_orientation.x = sin(phiRad);
		m_orientation.y = cos(phiRad) * cos(thetaRad);
		m_orientation.z = cos(phiRad) * sin(thetaRad);
	}

	//si vertical axe i greque
	else if (m_verticalAxe.y == 1.0)
	{
		m_orientation.x = cos(phiRad) * sin(thetaRad);
		m_orientation.y = sin(phiRad);
		m_orientation.z = cos(phiRad) * cos(thetaRad);
	}
	
	//si c'est le zed
	if (m_verticalAxe.z == 1.0)
	{
		m_orientation.x = cos(phiRad) * cos(thetaRad);
		m_orientation.y = cos(phiRad) * sin(thetaRad);
		m_orientation.z = sin(phiRad);
	}

	//calcule de la normale (back to termonale)
	m_lateralMove = cross(m_verticalAxe, m_orientation);
	m_lateralMove = normalize(m_lateralMove);

	//calcul du targetpoint pour openGLLLL

	m_targetPoint = m_position + m_orientation;
}

void	Camera::move(Input const &input)
{
	//gestion de l'orient
	if (input.getMouseMove())
		orient(input.getXRel(), input.getYRel());

	//vers l'avant
	if (input.getInput(SDL_SCANCODE_UP))
	{
		m_position = m_position + m_orientation * m_speed;
		m_targetPoint = m_position + m_orientation;
	}
	//vers l'arrierezrz
	if (input.getInput(SDL_SCANCODE_DOWN))
	{
		m_position = m_position - m_orientation * m_speed;
		m_targetPoint = m_position + m_orientation;
	}
	
	if (input.getInput(SDL_SCANCODE_LEFT))
	{
		m_position = m_position + m_lateralMove * m_speed;
		m_targetPoint = m_position + m_orientation;
	}
	
	if (input.getInput(SDL_SCANCODE_RIGHT))
	{
		m_position = m_position - m_lateralMove * m_speed;
		m_targetPoint = m_position + m_orientation;
	}
	std::cout << glm::to_string(m_position) << std::endl;
}

void	Camera::lookAt(glm::mat4 &modelview)
{
	//actualisation vue
	modelview = glm::lookAt(m_position, m_targetPoint, m_verticalAxe);
}


void	Camera::setTargetPoint(glm::vec3 targetPoint)
{
	//calcul veteur orientation
	m_orientation = m_targetPoint - m_position;
	m_orientation = normalize(m_orientation);

	//si axe vertical est x
	if (m_verticalAxe.x == 1.0)
	{
		//calcul angles
		m_phi = asin(m_orientation.x);
		m_theta = acos(m_orientation.y / cos(m_phi));
		
		if (m_orientation.y < 0)
			m_theta *= -1;
	}

	//si c'est le y

	if (m_verticalAxe.y == 1.0)
	{
		m_phi = asin(m_orientation.y);
		m_theta = acos(m_orientation.z / cos(m_phi));

		if (m_orientation.z < 0)
			m_theta *= -1;
	}

	//si c'est z
	if (m_verticalAxe.z == 1.0)
	{
		m_phi = asin(m_orientation.x);
		m_theta = acos(m_orientation.z / cos(m_phi));

		if (m_orientation.z < 0)
			m_theta *= -1;
	}
	
	//convert en degrés
	m_phi = m_phi * 180 / M_PI;
	m_theta = m_theta * 180 / M_PI;
}

void	Camera::setPosition(glm::vec3 position)
{
	// maj dde la pos
	m_position = position;

	// maj du point cible

	m_targetPoint = m_position + m_orientation;
}



float	Camera::getSensib() const
{
	return m_sensib;
}

float	Camera::getSpeed() const
{
	return m_speed;
}


void	Camera::setSensib(float sensib)
{
	m_sensib = sensib;
}

void	Camera::setSpeed(float speed)
{
	m_speed = speed;
}