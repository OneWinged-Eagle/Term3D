#include "Cube.h"

Cube::Cube(float size, std::string const shaderVertex, std::string const shaderFragment)
	: m_shader(shaderVertex, shaderFragment)
{
	m_shader.load();

	size /= 2;

	float verticesTmp[] = { -size, -size, -size,   size, -size, -size,   size, size, -size,
		-size, -size, -size,   -size, size, -size,   size, size, -size, 

		size, -size, size,   size, -size, -size,   size, size, -size,
		size, -size, size,   size, size, size,   size, size, -size,
		
		-size, -size, size,   size, -size, size,   size, -size, -size,
		-size, -size, size,   -size, -size, -size,   size, -size, -size,
		
		-size, -size, size,   size, -size, size,   size, size, size,
		-size, -size, size,   -size, size, size,   size, size, size,

		-size, -size, -size,   -size, -size, size,   -size, size, size,
		-size, -size, -size,   -size, size, -size,   -size, size, size,

		-size, size, size,   size, size, size,   size, size, -size,
		-size, size, size,   -size, size, -size,   size, size, -size };

																			 
	// Couleurs temporaires

	float colorsTmp[] = { 1.0, 0.0, 0.0,   1.0, 0.0, 0.0,   1.0, 0.0, 0.0,
		1.0, 0.0, 0.0,   1.0, 0.0, 0.0,   1.0, 0.0, 0.0, 

		0.0, 1.0, 0.0,   0.0, 1.0, 0.0,   0.0, 1.0, 0.0, 
		0.0, 1.0, 0.0,   0.0, 1.0, 0.0,   0.0, 1.0, 0.0, 

		0.0, 0.0, 1.0,   0.0, 0.0, 1.0,   0.0, 0.0, 1.0, 
		0.0, 0.0, 1.0,   0.0, 0.0, 1.0,   0.0, 0.0, 1.0,

		1.0, 0.0, 0.0,   1.0, 0.0, 0.0,   1.0, 0.0, 0.0,
		1.0, 0.0, 0.0,   1.0, 0.0, 0.0,   1.0, 0.0, 0.0,

		0.0, 1.0, 0.0,   0.0, 1.0, 0.0,   0.0, 1.0, 0.0,
		0.0, 1.0, 0.0,   0.0, 1.0, 0.0,   0.0, 1.0, 0.0,

		0.0, 0.0, 1.0,   0.0, 0.0, 1.0,   0.0, 0.0, 1.0,
		0.0, 0.0, 1.0,   0.0, 0.0, 1.0,   0.0, 0.0, 1.0 };


	for (int i(0); i < 108; i++)
	{
		m_vertices[i] = verticesTmp[i];
		m_colors[i] = colorsTmp[i];
	}
}


Cube::~Cube()
{

}

void Cube::display(glm::mat4 &projection, glm::mat4 &modelview)
{

	glUseProgram(m_shader.getProgramID());

	glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 0, m_vertices);
	glEnableVertexAttribArray(0);

	glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, 0, m_colors);
	glEnableVertexAttribArray(1);

	glUniformMatrix4fv(glGetUniformLocation(m_shader.getProgramID(), "projection"), 1, GL_FALSE, value_ptr(projection));
	glUniformMatrix4fv(glGetUniformLocation(m_shader.getProgramID(), "modelview"), 1, GL_FALSE, value_ptr(modelview));


	glDrawArrays(GL_TRIANGLES, 0, 36);


	glDisableVertexAttribArray(1);
	glDisableVertexAttribArray(0);

	glUseProgram(0);
}

