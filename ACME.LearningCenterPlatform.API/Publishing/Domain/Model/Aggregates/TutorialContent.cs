using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Entities;
using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.ValueObjects;

namespace ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Aggregates;

public partial class Tutorial : IPublishable
{
    
    public ICollection<Asset> Assets { get; }
    
    public EPublishingStatus Status { get; protected set; }

    public bool Readable => HasReadableAssets;

    public bool Viewable => HasViewableAssets;

    public bool HasReadableAssets => Assets.Any(asset => asset.Readable);

    public bool HasViewableAssets => Assets.Any(asset => asset.Viewable);

    private bool HasAllAssetsWithStatus(EPublishingStatus status) => 
        Assets.All(asset => asset.Status == status);
    
    
    public void SendToEdit()
    {
        if (HasAllAssetsWithStatus(EPublishingStatus.ReadyToEdit))
            Status = EPublishingStatus.ReadyToEdit;
    }

    public void SendToApproval()
    {
        if (HasAllAssetsWithStatus(EPublishingStatus.ReadyToApproval))
            Status = EPublishingStatus.ReadyToApproval;
    }

    public void ApproveAndLock()
    {
        if (HasAllAssetsWithStatus(EPublishingStatus.ApprovedAndLocked))
            Status = EPublishingStatus.ApprovedAndLocked;
    }

    public void Reject() => Status = EPublishingStatus.Draft;

    public void ReturnToEdit() => Status = EPublishingStatus.ReadyToEdit;

    public List<ContentItem> GetContent()
    {
        var content = new List<ContentItem>();
        if (Assets.Count > 0)
            content.AddRange(Assets.Select(asset => 
                new ContentItem(asset.Type.ToString(), asset.GetContent() as string 
                                                       ?? string.Empty)));
        return content;
    }

    private bool ExistsImageWithUrl(string imageUrl)
    {
        return Assets.Any(asset => asset.Type == EAssetType.Image &&
                                   (string)asset.GetContent() == imageUrl);
    }

    public void AddImage(string imageUrl)
    {
        if (ExistsImageWithUrl(imageUrl)) return;
        Assets.Add(new ImageAsset(imageUrl));
    }

    private bool ExistsVideoWithUrl(string videoUrl)
    {
        return Assets.Any(asset => asset.Type == EAssetType.Video &&
                                   (string)asset.GetContent() == videoUrl);
    }

    public void AddVideo(string videoUrl)
    {
        if (ExistsVideoWithUrl(videoUrl)) return;
        Assets.Add(new VideoAsset(videoUrl));
    }


    private bool ExistsReadableContent(string content)
    {
        return Assets.Any(asset => asset.Type == EAssetType.ReadableContentItem &&
                                   (string)asset.GetContent() == content);
    }

    public void AddReadableContent(string content)
    {
        if (ExistsReadableContent(content)) return;
        Assets.Add(new ReadableContentAsset(content));
    }

    public void RemoveAsset(AcmeAssetIdentifier identifier)
    {
        var asset = Assets.FirstOrDefault(asset => asset.AssetIdentifier == identifier);
        if (asset != null) Assets.Remove(asset);
    }

    public void ClearAssets() => Assets.Clear();
}