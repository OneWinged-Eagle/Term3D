#ifndef _SOL
#define _SOL

#include <GL\glew.h>

#include <glm\glm.hpp>
#include <glm\gtc\type_ptr.hpp>
#include <glm\gtx\transform.hpp>

#include "Shader.h"
#include "Texture.h"

class Sol
{
public:
	Sol(float length, float width, int roomLength, int roomWidth, std::string const shaderVertex, std::string const shaderFragment, std::string const texture);
	~Sol();

	void	display(glm::mat4 &projection, glm::mat4 &modelview);

private:

	Shader  m_shader;
	Texture  m_texture;

	float m_vertices[18];
	float m_textureCoord[12];

};

#endif // !_SOL
