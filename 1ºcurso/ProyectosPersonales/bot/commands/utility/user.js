const { SlashCommandBuilder } = require('discord.js');

module.exports = {
	data: new SlashCommandBuilder()
		.setName('user')
		.setDescription('Provides information about the user.'),
	async execute(interaction) {
		await interaction.reply(`Hola ${interaction.user.username}, Entrastes en el server: ${interaction.member.joinedAt}.`);
	},
};