const { joinVoiceChannel } = require('@discordjs/voice');
const { SlashCommandBuilder,ChannelType, MessageFlags } = require('discord.js');

module.exports = {
	data: new SlashCommandBuilder()
		.setName('join')
		.setDescription('entra a un canal')
        .addChannelOption((option) => option
                .setName('canal')
                .setDescription(`Canal necesario`)
                .setRequired(true)
                .addChannelTypes(ChannelType.GuildVoice)),
	async execute(interaction) {
		const VoiceChannel = interaction.options.getChannel('canal')
        const conection = joinVoiceChannel({
            channelId: VoiceChannel.id,
            guildId: interaction.guildId,
            adapterCreator: interaction.guild.voiceAdapterCreator
        });
        interaction.reply({content:"Conectado", flags: MessageFlags.Ephemeral});
	},
};