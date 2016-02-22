#ifndef _CAMERA
#define	_CAMERA

#include <glm\glm.hpp>
#include <glm\gtx\transform.hpp>
#include <glm\gtc\type_ptr.hpp>
#include <glm\gtx\string_cast.hpp>

#include <iostream>

#include "Input.h"


class Camera
{
public:
	Camera();
	Camera(glm::vec3 position, glm::vec3 targetPoint, glm::vec3 verticalAxe, float sensib, float speed);
	~Camera();

	void orient(int xRel, int YRel);
	void move(Input const &input);
	void lookAt(glm::mat4 &modelview);

	void setTargetPoint(glm::vec3 targetPoint);
	void setPosition(glm::vec3 position);

	float getSensib() const;
	float getSpeed() const;

	void setSensib(float sensib);
	void setSpeed(float speed);

private:
	float m_phi;
	float m_theta;
	glm::vec3 m_orientation;

	glm::vec3 m_verticalAxe;
	glm::vec3 m_lateralMove;

	glm::vec3 m_position;
	glm::vec3 m_targetPoint;

	float m_sensib;
	float m_speed;

};

#endif // !_CAMERA

