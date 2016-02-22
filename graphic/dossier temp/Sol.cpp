#include "Sol.h"

Sol::Sol(float length, float width, int roomLength, int roomWidth, std::string const shaderVertex, std::string const shaderFragment, std::string texture)
	:m_shader(shaderVertex, shaderFragment), m_texture(texture)
{
	m_shader.load();
	m_texture.load();

	length /= 2.0;
	width /= 2.0;

	//temp vertices
	float verticesTmp[] = { -length, 0, -width, length, 0, -width, length, 0, width,     // Triangle 1 
		-length, 0, -width, -length, 0, width,   length, 0, width };    // Triangle 2


	float textureCoord[] = { 0, 0,   roomLength, 0,   roomLength, roomWidth,
		0, 0,   0, roomWidth,   roomLength, roomWidth };

	for (int i = 0; i < 18; i++)
		m_vertices[i] = verticesTmp[i];
	for (int i = 0; i < 12; i++)
		m_textureCoord[i] = textureCoord[i];
}

Sol::~Sol()
{

}

void Sol::display(glm::mat4 &projection, glm::mat4 &modelview)
{
	//activeation shader
	glUseProgram(m_shader.getProgramID());

	//send des vertices
	glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 0, m_vertices);
	glEnableVertexAttribArray(0);

	//envoi des coord textures
	glVertexAttribPointer(2, 2, GL_FLOAT, GL_FALSE, 0, m_textureCoord);
	glEnableVertexAttribArray(2);

	//send des matrics
	glUniformMatrix4fv(glGetUniformLocation(m_shader.getProgramID(), "projection"), 1, GL_FALSE, value_ptr(projection));
	glUniformMatrix4fv(glGetUniformLocation(m_shader.getProgramID(), "modelview"), 1, GL_FALSE, value_ptr(modelview));

	//bind de la texture
	glBindTexture(GL_TEXTURE_2D, m_texture.getID());

	//render
	glDrawArrays(GL_TRIANGLES, 0, 6);

	//unvind texture
	glBindTexture(GL_TEXTURE_2D, 0);

	//desactivation tabs
	glDisableVertexAttribArray(2);
	glDisableVertexAttribArray(0);

	//unset du shader
	glUseProgram(0);
}