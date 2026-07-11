const {getVoiceConnection, generateDependencyReport } = require('@discordjs/voice');
const { SlashCommandBuilder, MessageFlags } = require('discord.js');

console.log(generateDependencyReport());

module.exports = {
	data: new SlashCommandBuilder()
		.setName('desconectar')
		.setDescription('sacar de un canal'),
	async execute(interaction) {
        const conection = getVoiceConnection(interaction.guild.id);
        conection.destroy();
        interaction.reply({content:"desconectado",  flags: MessageFlags.Ephemeral});
	},
};