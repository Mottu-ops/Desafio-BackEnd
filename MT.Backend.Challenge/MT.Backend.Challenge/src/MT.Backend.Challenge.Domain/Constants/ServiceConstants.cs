namespace MT.Backend.Challenge.Domain.Constants
{
    public static class ServiceConstants
    {
        // Settings Service Info

        public const string DeliveryDriverService = "DeliveryDriverService";
        public const string MotorcycleService = "MotorcycleService";

        // Default Messages
        public const string DefaultSuccessMessage = "Operação efetuada com sucesso";
        public const string DefaultErrorMessage = "Ocorreu um erro na execução da operação";
        public const string NotFoundMessage = "Item não encontrado";
        public const string DocumentAlreadyExists = "CNPJ ja esta Cadastrado";
        public const string DeliveryDriverNotFound = "Entregador não encontrado";
        public const string DeliveryDriverLicenseCategoryInvalid = "Tipo de CNH inválido para locação";
        public const string SendImageFail = "Falha ao enviar a imagem.";
        public const string ItemQueueRegistred = "Item registrado para a moto ID{0}";
        public const string NotificationStored = "Notificação armazenada com sucesso";
    }
}
