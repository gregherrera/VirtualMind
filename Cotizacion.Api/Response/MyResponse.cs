namespace Cotizacion.Api.Response
{
	public class MyResponse
	{
		public MyResponse()
		{
			this.Success = 0;
			this.Message = string.Empty;
			this.Data = null;
		}

		public int Success { get; set; }
		public string Message { get; set; }
		public object Data { get; set; }
	}
}
