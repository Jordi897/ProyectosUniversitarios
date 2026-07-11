const { SlashCommandBuilder } = require('discord.js');

module.exports = {
	data: new SlashCommandBuilder()
		.setName('server')
		.setDescription('Provides information about the server.'),
	async execute(interaction) {
		await interaction.reply(`Este server es de ${interaction.guild.name} y tiene ${interaction.guild.memberCount} miembros.`);
	},
};