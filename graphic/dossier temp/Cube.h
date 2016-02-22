#ifndef _CUBE
#define _CUBE

#include <GL\glew.h>

#include <glm\glm.hpp>
#include <glm\gtc\type_ptr.hpp>
#include <glm\gtx\transform.hpp>

#include "Shader.h"

class Cube
{
public:
	Cube(float size, std::string const shaderVertex, std::string const shaderFragment);
	~Cube();

	void display(glm::mat4 &projection, glm::mat4 &modelview);

private:
	Shader m_shader;
	float m_vertices[108];
	float m_colors[108];

};

#endif // !_CUBE
